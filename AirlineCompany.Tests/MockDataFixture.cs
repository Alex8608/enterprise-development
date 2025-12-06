using AirlineCompany.Core.Entities;

namespace AirlineCompany.Tests;

public class MockDataFixture
{
    public List<AircraftFamily> AircraftFamilies { get; private set; }
    public List<AircraftModel> AircraftModels { get; private set; }
    public List<Flight> Flights { get; private set; }
    public List<Passenger> Passengers { get; private set; }
    public List<Ticket> Tickets { get; private set; }

    public MockDataFixture()
    {
        AircraftFamilies = MockData.GetAircraftFamilies();
        AircraftModels = MockData.GetAircraftModels();
        Flights = MockData.GetFlights();
        Passengers = MockData.GetPassengers();
        Tickets = MockData.GetTickets(Flights, Passengers);
    }
}

