namespace Fintranet.Services.CongestionTax.Domain.WorkingCalendarAggregate;

public class WorkingCalendar : Entity, IAggregateRoot
{
    private WorkingCalendar() { }
    public WorkingCalendar(string workingCalendarName,DateOnly startDate,DateOnly endDate) : base()
    {
        WorkingCalendarName = workingCalendarName.NotNullOrWhiteSpace();
        SetWorkingDayes( DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday|DaysOfWeek.Thursday | DaysOfWeek.Friday);
        _holidays = new List<DateOnly>();
        StartDate= startDate; 
        EndDate= endDate;
    }

    public string WorkingCalendarName { get;private set; }
    public List<DateOnly> _holidays;

    public IReadOnlyCollection<DateOnly> Holidays => _holidays;
    public DaysOfWeek WorkingDays { get; private set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    // Domain Services
    public void SetWorkingDayes(DaysOfWeek workingDays)
    {
        this.WorkingDays= workingDays;       
    }

    public void AddHoliday(DateOnly date)
    {
        if(_holidays.Contains(date))
        {
            throw new CongestionTaxDomainException("This date has already been added as a holiday");
        }
        _holidays.Add(date);
    }
    public void RemoveHoliday(DateOnly date)
    {
        if (!_holidays.Contains(date))
        {
            throw new CongestionTaxDomainException("Is the date not already registered in the holiday list?");
        }
        _holidays.Remove(date);
    }

    public bool IsDateInHolidays(DateOnly date) => _holidays.Any(p => p==date);
}