

namespace Fintranet.Services.CongestionTax.Contracts.Interfaces;

public interface ICongestionTaxErrorHandler
{
  JsonErrorResponse GetError(Exception Exception);
}
