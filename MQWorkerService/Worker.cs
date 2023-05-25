using MessageHandlerAPIs.Contracts;

namespace MQWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMQConsumer _mqConsumer;

        public Worker(ILogger<Worker> logger, IMQConsumer mQConsumer)
        {
            _logger = logger;
            _mqConsumer = mQConsumer;
            _mqConsumer.AddExchange("demo").AddQueue("demoQueue").QueueBind("demoQueue", "demo");
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _mqConsumer.MessageReceived += HandleMessage;

            _mqConsumer.Consume("demoQueue");

            while (!stoppingToken.IsCancellationRequested)
            {
            };
        }

        private void HandleMessage(string arg)
        {
            Console.WriteLine(arg);
        }
    }
}