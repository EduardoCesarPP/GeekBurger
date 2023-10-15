
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Newtonsoft.Json;
using GeekBurger.Products.Repository;
using GeekBurger.Shared.Model;
using GeekBurger.Shared.Service;

namespace GeekBurger.Products.Contract.Service
{
    public class OrderChangedService : IOrderChangedService
    {
        private const string Fila = "OrderResults";
        private const string FilaNovasOrdens = "newOrderProduction";
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        private readonly List<ServiceBusMessage> _messages;
        private Task _lastTask;
        private readonly ILogService _logService;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }
        public OrderChangedService(IMapper mapper, IConfiguration configuration, ILogService logService, IServiceProvider serviceProvider)
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
            if (!await adminClient.TopicExistsAsync(Fila))
                await adminClient.CreateQueueAsync(Fila);
        }
        public async Task<string> CheckNewOrders()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return "Pending task";

            var messages = new List<string>();
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var receiver = client.CreateReceiver(FilaNovasOrdens, new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
            });

            ServiceBusReceivedMessage message;
            while ((message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5))) != null)
            {
                var text = message.Body.ToString();
                messages.Add(text);
                _logService.SendMessagesAsync($"Novo pedido: {text}");
            }
            receiver.CloseAsync();

            return messages.Count == 0 ? "" : $"[{String.Join(',', messages)}]";
        }

        public async void SendMessagesAsync(OrderChange onderChange)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            _logService.SendMessagesAsync("Order was changed");

            var topicSender = client.CreateSender(Fila);

            var orderChangedSerialized = JsonConvert.SerializeObject(onderChange);
            var orderChangedBinaryData = new BinaryData(Encoding.UTF8.GetBytes(orderChangedSerialized));

            var requestMsg = new ServiceBusMessage()
            {
                Body = orderChangedBinaryData,
                MessageId = Guid.NewGuid().ToString(),
                Subject = onderChange.OrderId.ToString(),

            };
            await topicSender.SendMessageAsync(requestMsg);

            var closeTask = topicSender.CloseAsync();
            await closeTask;
            HandleException(closeTask);
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