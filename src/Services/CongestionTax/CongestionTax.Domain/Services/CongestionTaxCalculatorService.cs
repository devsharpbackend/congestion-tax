
namespace Fintranet.Services.CongestionTax.Domain.Services;

public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
{
    private readonly IWorkingCalendarRepository _workingCalendarRepository;

    public CongestionTaxCalculatorService(IWorkingCalendarRepository workingCalendarRepository)
    {
        _workingCalendarRepository = workingCalendarRepository;
    }

    public async Task<decimal> GetTax(Vehicle vehicle, DateTime[] dates, City city)
    {
        city.NotNull();
        vehicle.NotNull();
        dates.NotNull();

        DateTime intervalStart = dates[0];
        decimal totalFee = 0;
        foreach (DateTime date in dates)
        {
            decimal nextFee = await GetTollFee(date, vehicle, city);
            decimal tempFee = await GetTollFee(intervalStart, vehicle, city);

            long diffInMollies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMollies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private async Task<decimal> GetTollFee(DateTime date, Vehicle vehicle, City city)
    {
        // get workingCalendar for city
        var workingCalendar = await this._workingCalendarRepository.GetById(city.WorkingCalendarId);
        workingCalendar.NotNull();

        if (workingCalendar.IsDateInHolidays(date.ToDateOnly()) || workingCalendar.IsDateInWorkingDays(date.ToDateOnly()) || city.IsVehicleTollFree(vehicle)) return 0;
        return city.GetChargeTariff(new TimeSpan(date.Hour, date.Minute, 0));
    }
}