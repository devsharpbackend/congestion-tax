
namespace Fintranet.Services.CongestionTax.Domain.Services;

public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
{
    private readonly IWorkingCalendarRepository _workingCalendarRepository;
    private readonly ILogger<CongestionTaxCalculatorService> _logger;
    public CongestionTaxCalculatorService(IWorkingCalendarRepository workingCalendarRepository,
        ILogger<CongestionTaxCalculatorService> logger)
    {
        _logger = logger.NotNull();
        _workingCalendarRepository = workingCalendarRepository.NotNull();
    }

    public async Task<decimal> GetTax(Vehicle vehicle, DateTime[] dates, City city)
    {
        city.NotNull();
        vehicle.NotNull();
        dates.NotNull();

        _logger.LogInformation("----- Calculate CongestionTax is Started for dates: {@dates}", dates);

        // is VehicleTollFree
        if (city.IsVehicleTollFree(vehicle))
            return 0;

        //To calculate tax we need workingCalendar
        var workingCalendar = await this._workingCalendarRepository.GetById(city.WorkingCalendarId);
        workingCalendar.NotNull();

        // We sort the array in ascending order
        Array.Sort(dates, (d1, d2) => DateTime.Compare(d1, d2));

        // the section we have to remove weekends,  holidays, days before a  holiday as per configuration and holiday month
        List<DateTime> dateList = new(dates);

        // Excluding the dates in which the tax is zero
        foreach (var date in dates)
        {
            if (IsTollFree(date))
                dateList.Remove(date);
        }
        // if all of dates had removed
        if (dateList.Count == 0)
            return 0;
        // Convert back to an array
        dates = dateList.ToArray();

        // if there is a date
        if (dates.Length == 1)
            return GetTollFee(dates[0]);

        //In this part, we need to cluster the dates by day 
        //Because we have to calculate the tax for each day and the following rule
        //The maximum amount per day and vehicle is 60 SEK.
        //So I wrote a function called ClusterByDay (In Shared Kernel Building Block) that returns a dictionary of days and dates
        Dictionary<int, List<DateTime>> clusters = dates.ClusterByDay();

        decimal totalTaxAmount = 0;
        foreach (KeyValuePair<int, List<DateTime>> cluster in clusters)
        {
            decimal taxPerDay = GetTaxPerDay(cluster.Value.ToArray());
            _logger.LogInformation("Get Tax for Day {@Key} is:{@taxPerDay}", cluster.Key, taxPerDay);
            totalTaxAmount += taxPerDay;
        }
        _logger.LogInformation("-----  CongestionTax  for dates: {@dates} is {@totalTaxAmount}", dates, totalTaxAmount);
        return totalTaxAmount;

        #region  Local functions

        decimal GetTaxPerDay(DateTime[] datesInDay)
        {
            DateTime intervalStart = datesInDay[0];
            decimal totalFee = 0;

            foreach (DateTime date in datesInDay)
            {
                decimal nextFee = GetTollFee(date);
                decimal tempFee = GetTollFee(intervalStart);

                TimeSpan difference = date.Subtract(intervalStart);
                int minutes = (int)difference.TotalMinutes;

                if (minutes <= city.SingleChargeIntervalInMinute)
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

            if (totalFee > city.MaxCongestionTaxPerDay) totalFee = city.MaxCongestionTaxPerDay;

            return totalFee;
        }

        decimal GetTollFee(DateTime date)
        {
            return city.GetChargeTariff(new TimeSpan(date.Hour, date.Minute, 0));
        }

        bool IsTollFree(DateTime date)
        {
            return workingCalendar.IsDateInHolidays(date.ToDateOnly()) // check for holidays
                || workingCalendar.IsDateInBeforeHolidays(date.ToDateOnly(), city.NumberOfDaysBeforeHoliday) // check for days before a public holiday
                || workingCalendar.IsMonthInHolidaysMonth(date.ToDateOnly()) // check for Holidays Month
                || !workingCalendar.IsDateInWorkingDays(date.ToDateOnly()); // check for is not WorkingDays -- for weekends

        }

        #endregion
    }


}