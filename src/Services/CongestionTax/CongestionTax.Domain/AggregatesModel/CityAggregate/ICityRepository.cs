namespace Fintranet.Services.CongestionTax.Domain.CityAggregate;

public interface ICityRepository : IRepository<City>
{
    City  Add(City item);
    void Update(City item);
    void Remove(City item);
    Task<City?> GetById(string id);
    Task<City?> GetByCityName(string roleName);

}
