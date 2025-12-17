using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Confluent.Kafka;
using System.Text.Json;

namespace AirlineCompany.Api.Kafka;

/// <summary>
/// Background service that consumes ticket events from Kafka and persists them to the database.
/// </summary>
public class KafkaConsumer(
    IConfiguration configuration,
    IConsumer<Ignore, string> consumer,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<KafkaConsumer> logger) : BackgroundService
{
    private readonly string _topic = configuration["KafkaTopic"] ?? "ticket-events";

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Main execution loop that continuously consumes Kafka messages and processes ticket events.
    /// </summary>
    /// <param name="stopToken">Cancellation token to stop the consumer gracefully.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stopToken)
    {
        consumer.Subscribe(_topic);
        logger.LogInformation("KafkaTicketConsumer is starting. Subscribing to topic: {Topic}", _topic);

        while (!stopToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(stopToken);

                if (string.IsNullOrEmpty(consumeResult?.Message?.Value))
                {
                    logger.LogWarning("An empty message was received");
                    continue;
                }

                var offset = consumeResult.TopicPartitionOffset;
                logger.LogDebug("Processing message at {Offset}", offset);

                var ticketDto = JsonSerializer.Deserialize<TicketCreateDto>(
                    consumeResult.Message.Value,
                    _jsonSerializerOptions);

                if (ticketDto == null)
                {
                    logger.LogWarning("Skipped invalid message at {Offset}: unable to deserialize", offset);
                    continue;
                }

                logger.LogInformation("Received ticket: FlightId={FlightId}, PassengerId={PassengerId}, Seat={Seat}, Baggage={Baggage}",
                    ticketDto.FlightId, ticketDto.PassengerId, ticketDto.SeatNumber, ticketDto.BaggageWeight);

                using var scope = serviceScopeFactory.CreateScope();

                var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

                try
                {
                    var ticketId = await ticketService.CreateTicket(ticketDto);

                    logger.LogInformation(
                        "Persisted ticket ID={TicketId} for FlightId={FlightId} with PassengerId={PassengerId}, Seat: {SeatNumber}",
                        ticketId, ticketDto.FlightId, ticketDto.PassengerId, ticketDto.SeatNumber);
                }
                catch (ArgumentException ex)
                {
                    logger.LogWarning("Ticket validation failed for FlightId={FlightId}, PassengerId={PassengerId}: {ErrorMessage}",
                        ticketDto.FlightId, ticketDto.PassengerId, ex.Message);

                    logger.LogDebug("Failed message content: {MessageContent}", consumeResult.Message.Value);
                    continue; 
                }

                consumer.Commit(consumeResult);
            }
            catch (ConsumeException ex)
            {
                logger.LogError(ex, "Kafka consumption failed");
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during message processing");
            }
        }

        consumer.Close();
        logger.LogInformation("Kafka consumer stopped");
    }
}