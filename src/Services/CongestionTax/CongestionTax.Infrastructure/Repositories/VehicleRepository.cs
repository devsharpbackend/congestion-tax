namespace Fintranet.Services.CongestionTax.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<VehicleRepository> _logger;

    public VehicleRepository(ApplicationDbContext context,  ILogger<VehicleRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IUnitOfWork UnitOfWork => _context;

    public Vehicle Add(Vehicle vehicle)=>  _context.Set<Vehicle>().Add(vehicle).Entity;

    public async Task<Vehicle?> GetById(string id)=> await _context.Set<Vehicle>()
    .SingleOrDefaultAsync(p=>p.Id==id);

    public async Task<Vehicle> GetByVehicleName(string vehicleName)=>
        await _context.Set<Vehicle>().SingleOrDefaultAsync(p => p.VehicleName.Contains(vehicleName));


       

    public void Remove(Vehicle vehicle) => _context.Set<Vehicle>().Remove(vehicle);

    public void Update(Vehicle vehicle) => _context.Entry(vehicle).State = EntityState.Modified;



}
