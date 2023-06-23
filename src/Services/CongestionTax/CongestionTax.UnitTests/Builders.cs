
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

public class CityBuilder
{
    private readonly City city;

    public CityBuilder()
    {
        WorkingCalendar workingCalendar = new("GothenburgWorkingCalendar", new DateOnly(2013, 1, 1), new DateOnly(2013, 12, 28));
        city = new("Gothenburg", workingCalendar, 2, 60, 60);
    }
    public CityBuilder AddTariff(decimal charge, TimeSpan fromTime, TimeSpan toTime)
    {
        city.AddTariff(charge, fromTime, toTime);
        return this;
    }
    public CityBuilder AddVehicle(Vehicle vehicle, bool isTollFree)
    {
        city.AddVehicle(vehicle, isTollFree);
        return this;
    }
   
    public CityBuilder SetWorkingCalendar(WorkingCalendar workingCalendar)
    {
        city.SetWorkingCalendar(workingCalendar);
        return this;
    }

    public City Build()
    {
        return city;
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
    public WorkingCalendarBuilder SetHolidaysMonth(Fintranet.Services.CongestionTax.Domain.Seedwork.MonthsOfYear months)
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