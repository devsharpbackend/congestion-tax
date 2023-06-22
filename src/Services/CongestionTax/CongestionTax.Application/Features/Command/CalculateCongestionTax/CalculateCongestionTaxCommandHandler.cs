namespace Fintranet.Services.CongestionTax.Application.Features; 

public class CalculateCongestionTaxCommandHandler : IRequestHandler<CalculateCongestionTaxCommand, decimal>
{
    private readonly ICityRepository _cityRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ILogger<CalculateCongestionTaxCommandHandler> _logger;
    private readonly ICongestionTaxCalculatorService _congestionTaxCalculatorService;
    public CalculateCongestionTaxCommandHandler(ICityRepository cityRepository, IVehicleRepository vehicleRepository, ILogger<CalculateCongestionTaxCommandHandler> logger,
        ICongestionTaxCalculatorService congestionTaxCalculatorService)
    {
        _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository)); ;
        _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository)); ;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _congestionTaxCalculatorService = congestionTaxCalculatorService ?? throw new ArgumentNullException(nameof(congestionTaxCalculatorService));
    }

    public async Task<decimal> Handle(CalculateCongestionTaxCommand request, CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.GetByCityName(request.CityName);
        if (city == null)
        {
            throw new NotFoundException(nameof(city), request.CityName);
        }

        Vehicle? vehicle = await _vehicleRepository.GetByVehicleName(request.VehicleName);
        if (vehicle == null)
        {
            throw new NotFoundException(nameof(vehicle), request.VehicleName);
        }

        decimal result = await _congestionTaxCalculatorService.GetTax(vehicle, request.CheckTimeList, city);

        _logger.LogInformation("----- Calculate CongestionTax for Vehicle: {@vehicle}", vehicle);

        return result;
    }

}
