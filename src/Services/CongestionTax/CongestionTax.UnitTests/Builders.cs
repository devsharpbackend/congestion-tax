namespace CongestionTax.Domain.UnitTest;

public class VehicleBuilder
{
    private readonly Vehicle vehicle;
    public VehicleBuilder(string vehicleName) {
        vehicle=new Vehicle(vehicleName);
    }

    public Vehicle Build()
    {
       return vehicle;
    }
}

public class WorkingCalendarBuilder
{
    private readonly WorkingCalendar workingCalendar;

    public WorkingCalendarBuilder()
    {
        workingCalendar = new("GothenburgWorkingCalendar", new DateOnly(2013, 1, 1), new DateOnly(2013, 12, 28));
       
    }
    public WorkingCalendarBuilder AddHoliday(DateOnly time)
    {
        workingCalendar.AddHoliday(time);
        return this;
    }
    public WorkingCalendarBuilder SetHolidaysMonth(Fintranet.Services.CongestionTax.Domain.Seedwork.MonthsOfYear months )
    {
        workingCalendar.SetHolidaysMonth(months);
       
        return this;
    }

    public WorkingCalendarBuilder SetWorkingDays(Fintranet.Services.CongestionTax.Domain.Seedwork.DaysOfWeek days)
    {
        workingCalendar.SetWorkingDays(days);
        return this;
    }

    public WorkingCalendar Build()
    {
        return workingCalendar;
    }
}

