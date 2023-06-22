namespace Fintranet.Services.CongestionTax.Domain.VehicleAggregate;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Vehicle Add(Vehicle item);
    void Update(Vehicle item);
    void Remove(Vehicle item);
    Task<Vehicle?> GetById(string id);
    Task<Vehicle?> GetByVehicleName(string vehicleName);
}