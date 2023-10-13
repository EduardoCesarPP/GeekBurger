

using Microsoft.Extensions.Configuration;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System.Text;

namespace GeekBurger.Production.Contract.Service
{
    public class ProductionService : IProductionService
    {
        private Dictionary<ServicoExterno, string> filasResponse = new Dictionary<ServicoExterno, string>
        {
            { ServicoExterno.ConsultarProdutos, "productsCatalogResponse"  } ,
            { ServicoExterno.ConsultarIngredientes, "ingredientsCatalogResponse"  },
            { ServicoExterno.ConsultarRestricoes, "restrictionsCatalogResponse"  }
        };
        private Dictionary<ServicoExterno, string> filasRequest = new Dictionary<ServicoExterno, string>
        {
            { ServicoExterno.ConsultarProdutos, "catalogProductsRequest"  } ,
            { ServicoExterno.ConsultarIngredientes, "catalogIngredientsRequest"  },
            { ServicoExterno.ConsultarRestricoes, "catalogRestrictionsRequest"  }
        };
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
            foreach (var Ingredient in filasResponse.Concat(filasRequest))
            {
                if (!await adminClient.QueueExistsAsync(Ingredient.Value))
                    await adminClient.CreateQueueAsync(Ingredient.Value);
            }
        }
        public async Task<string> SolicitarDados(string key, ServicoExterno servicoExterno)
        {
            Guid sessionId = Guid.NewGuid();
            await SendRequestMessage(key, sessionId.ToString(), servicoExterno);
            return await ReceiveReplyMessage(sessionId.ToString(), servicoExterno);
        }
        public async Task SendRequestMessage(string msg, string sessionId, ServicoExterno servicoExterno)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var queue = (filasRequest.Where(f => f.Key == servicoExterno).FirstOrDefault()).Value;
            var sender = client.CreateSender(queue);
            var requestMsg = new ServiceBusMessage(msg);
            requestMsg.SessionId = sessionId;
            await sender.SendMessageAsync(requestMsg);
        }
        public async Task<string> ReceiveReplyMessage(string sessionId, ServicoExterno servicoExterno)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return "Pending Task";

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var queue = (filasResponse.Where(f => f.Key == servicoExterno).FirstOrDefault()).Value;
            var receiver = await client.AcceptSessionAsync(queue, sessionId);
            var replyMsg = await receiver.ReceiveMessageAsync();
            if (replyMsg != null)
            {
                return replyMsg.Body.ToString();
            }
            else
            {
                throw new Exception("Failed to get reply from server");
            }
        }


        public async Task<List<string>>? CheckOrders(string sessionId, ServicoExterno servicoExterno)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return null;

            var messages = new List<string>();
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var receiver = await client.AcceptSessionAsync("newOrderProduction", sessionId);

            ServiceBusReceivedMessage message;
            while ((message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5))) != null)
            {
                var text = message.Body.ToString();
                messages.Add(text);
                _logService.SendMessagesAsync($"Novo pedido: {text}");
            }
            return messages;
        }

        public async Task<List<string>>? CheckOrderChanged()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return null;

            var messages = new List<String>();
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var client = new ServiceBusClient(config.ConnectionString);

            var receiver = client.CreateReceiver("orderResults", new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
            });

            ServiceBusReceivedMessage message;
            while ((message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5))) != null)
            {
                messages.Add(message.Body.ToString());
            }
            return messages;
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