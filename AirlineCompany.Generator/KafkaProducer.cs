using AirlineCompany.Generator.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AirlineCompany.Generator;

/// <summary>
/// Background service that generates fake ticket events and publishes them to a Kafka topic.
/// </summary>
public class KafkaProducer(
    IOptions<GeneratorOptions> settings,
    IProducer<Null, string> producer,
    ILogger<KafkaProducer> logger
) : BackgroundService
{
    /// <summary>
    /// Continuously generates and publishes ticket events to Kafka at configured intervals.
    /// </summary>
    /// <param name="stopToken">Cancellation token to stop the producer gracefully.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stopToken)
    {
        logger.LogInformation(
            "KafkaProducer started with IntervalMs={IntervalMs}, BatchSize={BatchSize}, Topic={Topic}",
            settings.Value.IntervalMs,
            settings.Value.BatchSize,
            settings.Value.Topic);

        while (!stopToken.IsCancellationRequested)
        {
            try
            {
                var tickets = TicketGenerator.GenerateTickets(settings.Value.BatchSize);

                foreach (var ticket in tickets)
                {
                    var json = JsonSerializer.Serialize(ticket);

                    logger.LogInformation("Generating ticket event: {TicketData}", json);

                    await producer.ProduceAsync(
                        settings.Value.Topic,
                        new Message<Null, string> { Value = json },
                        stopToken);
                }

                await Task.Delay(settings.Value.IntervalMs, stopToken);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("KafkaProducer received cancellation request");
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "KafkaProducer failed to produce message to Kafka topic");

                await Task.Delay(5000, stopToken);
            }
        }

        logger.LogInformation("KafkaProducer stopped");
    }
}