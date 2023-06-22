namespace Fintranet.Services.CongestionTax.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CityRepository> _logger;

    public CityRepository(ApplicationDbContext context,  ILogger<CityRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IUnitOfWork UnitOfWork => _context;

    public City Add(City city)=>  _context.Set<City>().Add(city).Entity;

    public async Task<City?> GetById(string id)=> await _context.Set<City>()
    .Include(p=>p.Tariffs)
    .Include(p => p.Vehicles)
    .SingleOrDefaultAsync(p=>p.Id==id);

    public async Task<City> GetByCityName(string cityName) => await _context.Set<City>()
    .Include(p => p.Tariffs)
    .Include(p => p.Vehicles)
    .SingleOrDefaultAsync(p => p.CityName == cityName);

    public void Remove(City city) => _context.Set<City>().Remove(city);

    public void Update(City city) => _context.Entry(city).State = EntityState.Modified;



}
