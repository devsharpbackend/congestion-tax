namespace Fintranet.Services.CongestionTax.Domain.WorkingCalendarAggregate;

public interface IWorkingCalendarRepository : IRepository<WorkingCalendar>
{
    WorkingCalendar Add(WorkingCalendar item);
    void Update(WorkingCalendar item);
    void Remove(WorkingCalendar item);
    Task<WorkingCalendar?> GetById(string id);
    Task<WorkingCalendar?> GetByWorkingCalendarName(string workingCalendarName);
}