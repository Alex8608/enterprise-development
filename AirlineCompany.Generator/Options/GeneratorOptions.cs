namespace AirlineCompany.Generator.Options;

/// <summary>
/// Configuration options for the ticket generator.
/// </summary>
public sealed class GeneratorOptions
{
    /// <summary>
    /// Interval between batches in milliseconds.
    /// </summary>
    public int IntervalMs { get; init; } = 5000;

    /// <summary>
    /// Number of tickets to generate in each batch.
    /// </summary>
    public int BatchSize { get; init; } = 1;

    /// <summary>
    /// Kafka topic for publishing tickets.
    /// </summary>
    public string Topic { get; init; } = "ticket-events";
}