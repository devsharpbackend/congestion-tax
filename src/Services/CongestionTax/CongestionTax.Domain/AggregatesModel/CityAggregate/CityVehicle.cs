namespace Fintranet.Services.CongestionTax.Domain.CityAggregate;

public class CityVehicle
{
    public CityVehicle(string vehicleId, string cityId, bool isTollFree)
    {
        VehicleId = vehicleId.NotNullOrWhiteSpace();
        CityId = cityId.NotNullOrWhiteSpace() ;
        IsTollFree = isTollFree;
    }

    public string VehicleId { get;private set; }
    public string CityId { get; private set; }
    public bool IsTollFree { get; private set; }
}