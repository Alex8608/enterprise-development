using AirlineCompany.Generator;
using AirlineCompany.Generator.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AirlineCompany.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection("Kafka"));

var kafkaConnection = builder.Configuration.GetConnectionString("KafkaConnection")
    ?? throw new InvalidOperationException("Kafka connection string is not configured.");

builder.Services.AddSingleton<IProducer<Null, string>>(serviceProvider =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = kafkaConnection,
        Acks = Acks.All,
        EnableIdempotence = true
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddHostedService<KafkaProducer>();

var host = builder.Build();
await host.RunAsync();