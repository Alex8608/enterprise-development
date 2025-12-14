namespace AirlineCompany.Dto;

/// <summary>
/// DTOs for analytic endpoints
/// </summary>
public static class AnalyticsDto { }

/// <summary>
/// Flight with passenger count
/// </summary>
public record FlightPassengerCountDto(string FlightCode, int PassengerCount);

/// <summary>
/// Flight with duration
/// </summary>
public record FlightDurationDto(string FlightCode, TimeSpan Duration);

/// <summary>
/// Passenger with zero baggage
/// </summary>
public record PassengerZeroBaggageDto(int PassengerId, string FullName, string PassportNumber);

/// <summary>
/// Flight statistics for model
/// </summary>
public record FlightStatisticsDto(
    int TotalFlights,
    int TotalPassengers,
    double AverageDurationHours,
    double TotalBaggageWeight);

/// <summary>
/// Flight by route
/// </summary>
public record FlightByRouteDto(
    string FlightCode,
    string DepartureCity,
    string ArrivalCity,
    DateTime DepartureDate,
    TimeSpan Duration);