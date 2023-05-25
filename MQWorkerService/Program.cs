using MessageHandlerAPIs;
using MessageHandlerAPIs.Contracts;
using MQWorkerService;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        
        services.AddScoped<IConnectionFactory>(cF =>
            new ConnectionFactory()
            {
                VirtualHost = "microhost",
                UserName = "ascendanceadmin",
                Password = "pass",
                HostName = "rabbitmq_cpu_ltd",
                //HostName = "83.229.84.17"

            });
        services.AddSingleton<IMQConsumer, MQConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
