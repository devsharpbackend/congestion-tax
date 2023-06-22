
namespace Fintranet.Services.CongestionTax.Infrastructure.Repositories;

public class WorkingCalendarRepository : IWorkingCalendarRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<VehicleRepository> _logger;

    public WorkingCalendarRepository(ApplicationDbContext context,  ILogger<VehicleRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public IUnitOfWork UnitOfWork => _context;
    public WorkingCalendar Add(WorkingCalendar workingCalendar)=>  _context.Set<WorkingCalendar>().Add(workingCalendar).Entity;
    public async Task<WorkingCalendar?> GetById(string id)=> await _context.Set<WorkingCalendar>()
    .SingleOrDefaultAsync(p=>p.Id==id);
    public async Task<WorkingCalendar> GetByWorkingCalendarName(string workingCalendarName) => 
        await _context.Set<WorkingCalendar>().SingleOrDefaultAsync(p => p.WorkingCalendarName == workingCalendarName);
    public void Remove(WorkingCalendar workingCalendar) => _context.Set<WorkingCalendar>().Remove(workingCalendar);
    public void Update(WorkingCalendar vehicle) => _context.Entry(vehicle).State = EntityState.Modified;



}
