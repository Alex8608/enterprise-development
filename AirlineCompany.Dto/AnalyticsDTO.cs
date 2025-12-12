namespace AirlineCompany.Dto;

/// <summary>
/// DTOs for analytic endpoints
/// </summary>
public static class AnalyticsDTO { }

/// <summary>
/// Flight with passenger count
/// </summary>
public record FlightPassengerCountDTO(string FlightCode, int PassengerCount);

/// <summary>
/// Flight with duration
/// </summary>
public record FlightDurationDTO(string FlightCode, TimeSpan Duration);

/// <summary>
/// Passenger with zero baggage
/// </summary>
public record PassengerZeroBaggageDTO(int PassengerId, string FullName, string PassportNumber);

/// <summary>
/// Flight statistics for model
/// </summary>
public record FlightStatisticsDTO(
    int TotalFlights,
    int TotalPassengers,
    double AverageDurationHours,
    double TotalBaggageWeight);

/// <summary>
/// Flight by route
/// </summary>
public record FlightByRouteDTO(
    string FlightCode,
    string DepartureCity,
    string ArrivalCity,
    DateTime DepartureDate,
    TimeSpan Duration);