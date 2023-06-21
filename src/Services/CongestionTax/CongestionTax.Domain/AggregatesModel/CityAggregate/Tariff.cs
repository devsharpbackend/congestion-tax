namespace Fintranet.Services.CongestionTax.Domain.CityAggregate;

public class Tariff : Entity
{
    private Tariff() { }
    public Tariff(string cityId, decimal charge, TimeSpan fromTime, TimeSpan toTime)
    {
        CityId = cityId.NotNullOrWhiteSpace();
        Charge = charge.NotNegative();
        FromTime = fromTime;
        ToTime = toTime;
    }

    public string CityId { get; private set; }
    public decimal Charge { get;private set; }

    public TimeSpan FromTime { get;private set; }
    public TimeSpan ToTime { get; private set; }
}