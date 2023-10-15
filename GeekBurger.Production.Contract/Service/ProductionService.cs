

using Microsoft.Extensions.Configuration;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System.Text;
using GeekBurger.Shared.Service;

namespace GeekBurger.Production.Contract.Service
{
    public class ProductionService : IProductionService
    { 
        private const string FilaNovasOrdens = "newOrderProduction";
        private const string FilaMudancas = "orderResults";
        private const string MicroService = "StoreCatalog";
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        private ILogService _logService;
        private readonly List<ServiceBusMessage> _messages;
        private Task _lastTask;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }
        public ProductionService(IMapper mapper, IConfiguration configuration, IServiceProvider serviceProvider, ILogService logService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _messages = new List<ServiceBusMessage>();

            _cancelMessages = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            EnsureTopicIsCreated();
            _logService = logService;
        }
        public async Task EnsureTopicIsCreated()
        {
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var adminClient = new ServiceBusAdministrationClient(config.ConnectionString);

            if (!await adminClient.QueueExistsAsync(FilaNovasOrdens))
                await adminClient.CreateQueueAsync(FilaNovasOrdens);

            if (!await adminClient.QueueExistsAsync(FilaMudancas))
                await adminClient.CreateQueueAsync(FilaMudancas);
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

        public async Task<string> CheckOrderChanges()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return "Pending task";

            var messages = new List<String>();
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var receiver = client.CreateReceiver(FilaMudancas, new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
            });

            ServiceBusReceivedMessage message;
            while ((message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5))) != null)
            {
                messages.Add(message.Body.ToString());
            }

            receiver.CloseAsync();

            return messages.Count == 0 ? "" : $"[{String.Join(',', messages)}]";
        }

        public async Task NotifyOrderEnding(string key, string result)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            _logService.SendMessagesAsync($"Pedido: {key} finalizado. Resultado: {result}.");
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