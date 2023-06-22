namespace Fintranet.Services.CongestionTax.Application.Features;

public class CalculateCongestionTaxCommand : BaseCommand<Decimal>
{
    public CalculateCongestionTaxCommand(string cityName, string vehicleName, DateTime[] checkTimeList)
    {
        CityName = cityName;
        VehicleName = vehicleName;
        CheckTimeList = checkTimeList;
    }

  public string CityName { get; set; }
  public string VehicleName { get; set; }
  public DateTime [] CheckTimeList { get; set; }
}
