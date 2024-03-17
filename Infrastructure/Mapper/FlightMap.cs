using Domain.Models;

namespace Infrastructre.Mapper;

using CsvHelper.Configuration;

internal sealed class FlightMap : ClassMap<Flight>
{
    public FlightMap()
    {
        Map(m => m.Id).Index(0);
        Map(m => m.DepartureDate).Index(1);
        Map(m => m.DepartureAirport).Index(2);
        Map(m => m.ArrivalAirport).Index(3);
    }
}
