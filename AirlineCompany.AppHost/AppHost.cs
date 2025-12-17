var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .AddDatabase("AirlineCompanyDb");

var kafka = builder.AddKafka("kafka")
    .WithDataVolume()
    .WithKafkaUI();

var kafkaTopic = builder.AddParameter("KafkaTopic", "ticket-events");
var producerIntervalMs = builder.AddParameter("KafkaProducerIntervalMs", "5000");
var producerBatchSize = builder.AddParameter("KafkaProducerBatchSize", "1");

var api = builder.AddProject<Projects.AirlineCompany_Api>("airline-api")
    .WithReference(sqlServer)
    .WithReference(kafka, "KafkaConnection")
    .WaitFor(sqlServer)
    .WaitFor(kafka)
    .WithExternalHttpEndpoints();

var generator = builder.AddProject<Projects.AirlineCompany_Generator>("airline-generator")
    .WithReference(kafka, "KafkaConnection")
    .WithEnvironment("KafkaTopic", kafkaTopic)
    .WithEnvironment("KafkaProducer__IntervalMs", producerIntervalMs)
    .WithEnvironment("KafkaProducer__BatchSize", producerBatchSize)
    .WaitFor(kafka);

api.WaitFor(generator);

builder.Build().Run();