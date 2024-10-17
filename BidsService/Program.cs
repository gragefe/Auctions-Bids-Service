using BidsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<KafkaConsumer>();

        services.AddSingleton<IKafkaConsumerHandler, KafkaConsumerHandler>();
    })
    .Build();

await host.RunAsync();