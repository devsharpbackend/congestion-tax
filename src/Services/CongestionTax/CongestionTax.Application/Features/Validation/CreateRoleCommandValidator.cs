namespace Fintranet.Services.CongestionTax.Application.Features.Validation;

public class CalculateCongestionTaxCommandValidator : AbstractValidator<CalculateCongestionTaxCommand>
{
    public CalculateCongestionTaxCommandValidator(ILogger<CalculateCongestionTaxCommandValidator> logger)
    {
        RuleFor(command => command.VehicleName).NotEmpty().WithMessage(string.Format(ValidationMessages.IS_EMPTY, "Vehicle Name"));
        RuleFor(command => command.CityName).NotEmpty().WithMessage(string.Format(ValidationMessages.IS_EMPTY, "City Name"));

        // check date list

        logger.LogTrace("----- Calculate Congestion Tax Command  INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}