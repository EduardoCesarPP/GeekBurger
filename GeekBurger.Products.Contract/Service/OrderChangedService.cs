
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using GeekBurger.Products.Contract.Model;
using Newtonsoft.Json;
using GeekBurger.Products.Repository;

namespace GeekBurger.Products.Contract.Service
{
    public class OrderChangedService : IOrderChangedService
    {
        private const string Topic = "OrderChangedTopic";
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        private readonly List<ServiceBusMessage> _messages;
        private Task _lastTask;
        //private readonly IServiceBusNamespace _namespace;
        private readonly ILogService _logService;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }

        public OrderChangedService(IMapper mapper,
            IConfiguration configuration, ILogService logService, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _configuration = configuration;
            _logService = logService;
            _messages = new List<ServiceBusMessage>();

            _cancelMessages = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            EnsureTopicIsCreated();
        }

        public async Task EnsureTopicIsCreated()
        {
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var adminClient = new ServiceBusAdministrationClient(config.ConnectionString);
            if (!await adminClient.TopicExistsAsync(Topic))
                await adminClient.CreateTopicAsync(Topic);
        }

        public void AddToMessageList(IEnumerable<EntityEntry<Order>> changes)
        {
            _messages.AddRange((IEnumerable<ServiceBusMessage>)changes
                .Where(entity => entity.State != EntityState.Detached
                                 && entity.State != EntityState.Unchanged)
                .Select(GetMessage).ToList());
        }

        private void AddOrUpdateEvent(OrderChangedEvent OrderChangedEvent)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IOrderChangedEventRepository>();

                    OrderChangedEvent evt;
                    if (OrderChangedEvent.EventId == Guid.Empty
                        || (evt = scopedProcessingService.Get(OrderChangedEvent.EventId)) == null)
                        scopedProcessingService.Add(OrderChangedEvent);
                    else
                    {
                        evt.MessageSent = true;
                        scopedProcessingService.Update(evt);
                    }

                    scopedProcessingService.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ServiceBusMessage GetMessage(EntityEntry<Order> entity)
        {
            var OrderChanged = _mapper.Map<OrderChangedMessage>(entity);

            //var OrderChanged = new OrderChangedMessage { Order = _mapper.Map<OrderToGet>(entity.Entity), State = OrderState.Paid };


            var OrderChangedSerialized = JsonConvert.SerializeObject(OrderChanged);
            var OrderChangedBinaryData = new BinaryData(Encoding.UTF8.GetBytes(OrderChangedSerialized));

            var OrderChangedEvent = _mapper.Map<OrderChangedEvent>(entity);
            AddOrUpdateEvent(OrderChangedEvent);

            return new ServiceBusMessage()
            {
                Body = OrderChangedBinaryData,
                MessageId = OrderChangedEvent.EventId.ToString(),
                Subject = OrderChanged.Order.StoreId.ToString(),

            };
        }

        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            _logService.SendMessagesAsync("Order was changed");

            var topicSender = client.CreateSender(Topic);
            _lastTask = SendAsync(topicSender, _cancelMessages.Token);

            await _lastTask;

            var closeTask = topicSender.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        public async Task SendAsync(ServiceBusSender topicSender,
            CancellationToken cancellationToken)
        {
            var tries = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_messages.Count <= 0)
                    break;

                ServiceBusMessage message;
                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                var sendTask = topicSender.SendMessageAsync(message, cancellationToken);
                await sendTask;
                var success = HandleException(sendTask);

                if (!success)
                {
                    var cancelled = cancellationToken.WaitHandle.WaitOne(10000 * (tries < 60 ? tries++ : tries));
                    if (cancelled) break;
                }
                else
                {
                    if (message == null) continue;
                    AddOrUpdateEvent(new OrderChangedEvent() { EventId = new Guid(message.MessageId) });
                    _messages.Remove(message);
                }
            }
        }

        public bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Console.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusException)
                    Console.WriteLine("Connection Problem with Host. Internet Connection can be down");
            });

            return false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            EnsureTopicIsCreated();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancelMessages.Cancel();

            return Task.CompletedTask;
        }
    }
}