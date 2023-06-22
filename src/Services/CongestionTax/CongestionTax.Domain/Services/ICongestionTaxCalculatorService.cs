namespace Fintranet.Services.CongestionTax.Domain.Services;

public interface ICongestionTaxCalculatorService
{
    Task<decimal> GetTax(Vehicle vehicle, DateTime[] dates, City city);
}