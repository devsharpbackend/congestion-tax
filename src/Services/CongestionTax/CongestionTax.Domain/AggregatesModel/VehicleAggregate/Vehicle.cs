namespace Fintranet.Services.CongestionTax.Domain.VehicleAggregate;

public class Vehicle : Entity, IAggregateRoot
{
    private Vehicle() { }
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