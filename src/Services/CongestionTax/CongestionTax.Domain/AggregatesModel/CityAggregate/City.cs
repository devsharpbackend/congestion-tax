namespace Fintranet.Services.CongestionTax.Domain.CityAggregate;

public class City : Entity, IAggregateRoot
{
    private City() {
        _tariffs = new List<Tariff>();
        _vehicles = new List<CityVehicle>(); 
    }
    public City(string cityName, WorkingCalendar workingCalendar) : base()
    {
        if (workingCalendar.StartDate.Year != 2013)
        {
            throw new CongestionTaxDomainException("Working calendar must be for the year 2013.");
        }
        if (workingCalendar.EndDate.Year != 2013)
        {
            throw new CongestionTaxDomainException("Working calendar must be for the year 2013.");
        }

        workingCalendar.NotNull(cityName);
        CityName = cityName.NotNullOrWhiteSpace();
        WorkingCalendarId = workingCalendar.Id;
        _tariffs = new List<Tariff>();
        _vehicles = new List<CityVehicle>();
    }

    public string CityName { get; private set; }
    public string WorkingCalendarId { get; private set; }


    private List<CityVehicle> _vehicles;
    public IReadOnlyCollection<CityVehicle> Vehicles => _vehicles;


    private List<Tariff> _tariffs;
    public IReadOnlyCollection<Tariff> Tariffs => _tariffs;


    // Domain Services List for Vehicles Managment
    public void AddVehicle(Vehicle vehicle, bool isTollFree)
    {
        vehicle.NotNull();
        CityVehicle? cityVehicle = Vehicles.FirstOrDefault(v => v.VehicleId == vehicle.Id);
        if (cityVehicle == null)
        {
            _vehicles.Add(new CityVehicle(vehicle.Id, this.Id,isTollFree));
        }
        throw new CongestionTaxDomainException("This vehicle is already registered");
    }
    public void RemoveVehicle(Vehicle vehicle)
    {
        vehicle.NotNull();
        CityVehicle? cityVehicle = Vehicles.FirstOrDefault(v => v.VehicleId == vehicle.Id);
        if (cityVehicle != null)
        {
            _vehicles.Remove(cityVehicle);
        }
        throw new CongestionTaxDomainException("This vehicle does not exist for deletion");
    }

    public bool IsVehicleTollFree(Vehicle vehicle)=> _vehicles.Any(p=>p.IsTollFree && p.VehicleId == vehicle.Id);

    // Domain Services List for Tariffs Managment
    public void AddTariff(decimal charge, TimeSpan fromTime, TimeSpan toTime)
    {
        // check isOverlap
        Tariff? tariff  = Tariffs.FirstOrDefault(t => fromTime < t.ToTime && toTime > t.FromTime);

        if (tariff == null)
        {
            _tariffs.Add(new Tariff(this.Id,charge,fromTime,toTime));
        }
        throw new CongestionTaxDomainException("This tariff In this range is already registered");
    }
    public void RemoveTariff(TimeSpan fromTime, TimeSpan toTime)
    {
        Tariff? tariff = Tariffs.FirstOrDefault(t => fromTime == t.ToTime && toTime == t.FromTime);
        if (tariff != null)
        {
            _tariffs.Remove(tariff);
        }
        throw new CongestionTaxDomainException("This tariff  In this range  does not exist for deletion");
    }

    public decimal GetChargeTariff(TimeSpan fromTime, TimeSpan toTime)
    {
        Tariff? tariff = Tariffs.FirstOrDefault(t => fromTime < t.ToTime && toTime > t.FromTime);
        if (tariff != null)
        {
            return tariff.Charge;
        }
        return 0;
    }
    public void ClearTariff()
    {
        _tariffs.Clear();
    }
}

