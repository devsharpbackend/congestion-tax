
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        // is VehicleTollFree
        if (city.IsVehicleTollFree(vehicle))
            return 0;

        // We sort the array in ascending order
        Array.Sort(dates, (d1, d2) => DateTime.Compare(d1, d2));

        // the section we have to remove weekends,  holidays, days before a  holiday as per configuration and holiday month
        List<DateTime> dateList = new(dates);

        // Remove items based on a condition
        // for remove we need to workingCalendar
        // so extract it
        var workingCalendar = await this._workingCalendarRepository.GetById(city.WorkingCalendarId);
        workingCalendar.NotNull();

        foreach (var date in dates)
        {
            if ( IsTollFree(date))
                dateList.Remove(date);
        }
        // if all of dates had removed
        if (dateList.Count == 0)
            return 0;


        DateTime intervalStart = dateList[0];
        dateList.RemoveAt(0);

        // Convert back to an array
        dates = dateList.ToArray();

        // if there is a date
        if (dates.Length == 1)
            return GetTollFee(intervalStart);


        decimal nextFee = 0;
        decimal tempFee = 0;
        nextFee = tempFee = GetTollFee(intervalStart);
        decimal totalFee = tempFee;

        foreach (DateTime date in dates)
        {
            nextFee = GetTollFee(date);
            tempFee = GetTollFee(intervalStart);

            TimeSpan difference = date.Subtract(intervalStart);
            int minutes = (int)difference.TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                //If it is higher than 60 minutes, the start date should be changed to the latest date
                intervalStart = date;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;


        //  Local functions
        decimal GetTollFee(DateTime date)
        {
            return city.GetChargeTariff(new TimeSpan(date.Hour, date.Minute, 0));
        }

        bool IsTollFree(DateTime date)
        {
            return workingCalendar.IsDateInHolidays(date.ToDateOnly())
                || workingCalendar.IsMonthInHolidaysMonth(date.ToDateOnly())
                || !workingCalendar.IsDateInWorkingDays(date.ToDateOnly());
        }
    }


}