namespace CongestionTax.Domain.UnitTests.AggregatesModel;

public class CityAggregateTest
{
    public CityAggregateTest()
    { }

    [Fact]
    public void InValid_GetChargeTariff()
    {
        City city = new CityBuilder()
            .AddTariff(8, new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59))
            .AddTariff(13, new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59))
            .AddTariff(18, new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59))
            .AddTariff(13, new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59))
            .AddTariff(8, new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59))
            .AddTariff(13, new TimeSpan(15, 00, 0), new TimeSpan(15, 29, 59))
            .AddTariff(18, new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59))
            .AddTariff(13, new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59))
            .AddTariff(8, new TimeSpan(18, 0, 0), new TimeSpan(17, 29, 59))
            .AddTariff(0, new TimeSpan(18, 30, 0), new TimeSpan(5, 59, 59))
            .Build();

        //Act - Assert
        Assert.NotEqual(8, city.GetChargeTariff(new TimeSpan(2, 15, 0)));
    }
    [Fact]
    public void InValid_WorkingCalendarIn2013()
    {

        City city = new CityBuilder()
            .Build();
        WorkingCalendar workingCalendar = new("testcalander", new DateOnly(2014, 1, 1), new DateOnly(2015, 1, 2));

        //Act - Assert
        Assert.Throws<CongestionTaxDomainException>(() => city.SetWorkingCalendar(workingCalendar));
    }

}