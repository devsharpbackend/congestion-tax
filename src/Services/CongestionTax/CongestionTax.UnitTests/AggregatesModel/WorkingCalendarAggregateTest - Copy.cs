namespace CongestionTax.Domain.UnitTests.AggregatesModel;

public class WorkingCalendarAggregateTest
{
    public WorkingCalendarAggregateTest()
    { }


    [Fact]
    public void Invalid_IsDateInWorkingDays()
    {
        // working days is Friday and Saturday
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder()
            .SetWorkingDays(Fintranet.Services.CongestionTax.Domain.Seedwork.DaysOfWeek.Saturday 
            | Fintranet.Services.CongestionTax.Domain.Seedwork.DaysOfWeek.Friday).Build();

        // this date is Wednesday
        DateOnly date = new (2013, 1, 2);

        //Act - Assert
        Assert.False( workingCalendar.IsDateInWorkingDays(date));
    }


    [Fact]
    public void Invalid_IsMonthInHolidaysMonth()
    {
        // set HolidaysMonth
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder()
            .SetHolidaysMonth(Fintranet.Services.CongestionTax.Domain.Seedwork.MonthsOfYear.April
            | Fintranet.Services.CongestionTax.Domain.Seedwork.MonthsOfYear.December)
            .Build();

        // this date is Wednesday
        DateOnly date = new(2013, 5, 1);

        //Act - Assert
        Assert.False(workingCalendar.IsMonthInHolidaysMonth(date));
    }
}