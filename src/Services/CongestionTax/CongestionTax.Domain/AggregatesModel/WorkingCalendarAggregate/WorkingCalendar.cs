namespace Fintranet.Services.CongestionTax.Domain.WorkingCalendarAggregate;

public class WorkingCalendar : Entity, IAggregateRoot
{
    private WorkingCalendar() { }
    public WorkingCalendar(string workingCalendarName, DateOnly startDate, DateOnly endDate) : base()
    {
        WorkingCalendarName = workingCalendarName.NotNullOrWhiteSpace();
        SetWorkingDays(DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday | DaysOfWeek.Friday);
        _holidays = new List<DateOnly>();
        StartDate = startDate;
        EndDate = endDate;
    }

    public string WorkingCalendarName { get; private set; }
    public List<DateOnly> _holidays;

    public IReadOnlyCollection<DateOnly> Holidays => _holidays;
    public DaysOfWeek WorkingDays { get; private set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    // Domain Services
    public void SetWorkingDays(DaysOfWeek workingDays)
    {
        this.WorkingDays = workingDays;
    }

    public void AddHoliday(DateOnly date)
    {
        if (_holidays.Contains(date))
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

    public bool IsDateInHolidays(DateOnly date) => _holidays.Any(p => p == date);
    public bool IsDateInWorkingDays(DateOnly date)
    {
        bool result = false;
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Monday: { result = (WorkingDays & DaysOfWeek.Monday) == DaysOfWeek.Monday; break; };
            case DayOfWeek.Tuesday: { result = (WorkingDays & DaysOfWeek.Tuesday) == DaysOfWeek.Tuesday; break; };
            case DayOfWeek.Wednesday: { result = (WorkingDays & DaysOfWeek.Wednesday) == DaysOfWeek.Wednesday; break; };
            case DayOfWeek.Thursday: { result = (WorkingDays & DaysOfWeek.Thursday) == DaysOfWeek.Thursday; break; };
            case DayOfWeek.Friday: { result = (WorkingDays & DaysOfWeek.Friday) == DaysOfWeek.Friday; break; };
            case DayOfWeek.Saturday: { result = (WorkingDays & DaysOfWeek.Saturday) == DaysOfWeek.Saturday; break; };
            case DayOfWeek.Sunday: { result = (WorkingDays & DaysOfWeek.Sunday) == DaysOfWeek.Sunday; break; };
        }

        return result;
    }
}