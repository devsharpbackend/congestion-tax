namespace Fintranet.Services.CongestionTax.Infrastructure.VehicleAggregate;

public class Vehicle : Entity, IAggregateRoot
{
    public Vehicle(string vehicleName) : base()
    {
        VehicleName = vehicleName.NotNullOrWhiteSpace();
    }
    public string VehicleName { get; private set; }

    public String GetVehicleType()
    {
        return VehicleName;
    }
}