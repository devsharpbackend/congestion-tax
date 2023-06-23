namespace CongestionTax.Domain.UnitTests.AggregatesModel;

public class CongestionTaxCalculatorServiceTest
{
    private readonly Mock<IWorkingCalendarRepository> _mockWorkingCalendarRepository;
    private readonly Mock<ILogger<CongestionTaxCalculatorService>> _mockLogger;
    public CongestionTaxCalculatorServiceTest()
    {
        _mockWorkingCalendarRepository = new();
        _mockLogger = new();
    }

    [Fact]
    public void InValid_GetTaxForPersonalVehicle()
    {
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder().Build();

        _mockWorkingCalendarRepository.Setup(workingRepo => workingRepo.GetById(null))
           .Returns(Task.FromResult<WorkingCalendar>(workingCalendar));

        CongestionTaxCalculatorService service = new(_mockWorkingCalendarRepository.Object, _mockLogger.Object);

        Vehicle vehicle = new VehicleBuilder("Personal").Build();
        vehicle.GetType().GetProperty("Id").SetValue(vehicle, "41797772-D137-4129-B8EB-FD7B3073CB4A", null); // set VehicleId

        DateTime[] dates = GetDates();
        City city = GetCity(vehicle);

        //Act - Assert
        Assert.NotEqual(100, service.GetTax(vehicle, dates, city).Result);
    }


    [Fact]
    public void Valid_GetTaxForEmergencyVehicle()
    {
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder().Build();

        _mockWorkingCalendarRepository.Setup(workingRepo => workingRepo.GetById(null))
           .Returns(Task.FromResult<WorkingCalendar>(workingCalendar));

        CongestionTaxCalculatorService service = new(_mockWorkingCalendarRepository.Object, _mockLogger.Object);

        Vehicle vehicle = new VehicleBuilder("Emergency").Build();
        vehicle.GetType().GetProperty("Id").SetValue(vehicle, "41797772-D137-4129-B8EB-FD7B3073CB4A", null);

        DateTime[] dates = GetDates();
        City city = GetCity(vehicle);

        //Act - Assert
        Assert.Equal(0, service.GetTax(vehicle, dates, city).Result);
    }

    [Fact]
    public void Valid_GetTaxForHoliday()
    {
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder().Build();
        workingCalendar.AddHoliday(new DateOnly(2013,2,28));

        _mockWorkingCalendarRepository.Setup(workingRepo => workingRepo.GetById(null))
           .Returns(Task.FromResult<WorkingCalendar>(workingCalendar));

        CongestionTaxCalculatorService service = new(_mockWorkingCalendarRepository.Object, _mockLogger.Object);

        Vehicle vehicle = new VehicleBuilder("Emergency").Build();
        vehicle.GetType().GetProperty("Id").SetValue(vehicle, "41797772-D137-4129-B8EB-FD7B3073CB4A", null);

        List<string> dateStrings = new(){
                "2013-02-28T08:00:00"};

        DateTime[] dates = dateStrings.ToDateTimeArray("yyyy-MM-dd'T'HH:mm:ss");

        City city = GetCity(vehicle);

        //Act - Assert
        Assert.Equal(0, service.GetTax(vehicle, dates, city).Result);
    }

      [Fact]
    public void Valid_GetTaxForDaysbeforeHoliday()
    {
        WorkingCalendar workingCalendar = new WorkingCalendarBuilder().Build();
        workingCalendar.AddHoliday(new DateOnly(2013,2,28));

        _mockWorkingCalendarRepository.Setup(workingRepo => workingRepo.GetById(null))
           .Returns(Task.FromResult<WorkingCalendar>(workingCalendar));

        CongestionTaxCalculatorService service = new(_mockWorkingCalendarRepository.Object, _mockLogger.Object);

        Vehicle vehicle = new VehicleBuilder("Emergency").Build();
        vehicle.GetType().GetProperty("Id").SetValue(vehicle, "41797772-D137-4129-B8EB-FD7B3073CB4A", null);

        List<string> dateStrings = new(){
                "2013-02-27T08:00:00"};

        DateTime[] dates = dateStrings.ToDateTimeArray("yyyy-MM-dd'T'HH:mm:ss");

        City city = GetCity(vehicle);

        //Act - Assert
        Assert.Equal(0, service.GetTax(vehicle, dates, city).Result);
    }

    private static DateTime[] GetDates()
    {
        List<string> dateStrings = new(){
                "2013-01-15T21:00:00",
                "2013-02-07T06:23:27",
                "2013-02-07T15:27:00",
                "2013-02-08T06:27:00",
                "2013-02-08T06:20:27",
                "2013-02-08T14:35:00",
                "2013-02-08T15:29:00",
                "2013-02-08T15:47:00",
                "2013-02-08T16:01:00",
                "2013-02-08T16:48:00",
                "2013-02-08T17:49:00",
                "2013-02-08T18:29:00",
                "2013-02-08T18:35:00",
                "2013-03-26T14:25:00",
                "2013-03-28T14:07:27"};

        DateTime[] dates = dateStrings.ToDateTimeArray("yyyy-MM-dd'T'HH:mm:ss");
        return dates;
    }
    private static City GetCity(Vehicle vehicle)
    {
        return new CityBuilder()
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
            .AddVehicle(vehicle, false)
            .Build();
    }

   


}