namespace Fintranet.Services.CongestionTax.Application.Features.Validation;

public class CalculateCongestionTaxCommandValidator : AbstractValidator<CalculateCongestionTaxCommand>
{
    public CalculateCongestionTaxCommandValidator(ILogger<CalculateCongestionTaxCommandValidator> logger)
    {
        RuleFor(command => command.VehicleName).NotEmpty().WithMessage(string.Format(ValidationMessages.IS_EMPTY, "Vehicle Name"));
        RuleFor(command => command.CityName).NotEmpty().WithMessage(string.Format(ValidationMessages.IS_EMPTY, "City Name"));

        // check date list
        RuleFor(command => command.CheckTimeList)
            .NotEmpty()
            .WithMessage("CheckTimeList cannot be empty.");

        RuleForEach(command=>command.CheckTimeList).Must(BeWithin2013)
          .WithMessage("The DateTime value must be within the year 2013.");


        logger.LogTrace("----- Calculate Congestion Tax Command  INSTANCE CREATED - {ClassName}", GetType().Name);
    }

    private bool BeWithin2013(DateTime dt)
    {
        return dt.Year == 2013;
    }
}