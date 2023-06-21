
namespace Fintranet.Services.CongestionTaxA.Application.Exceptions;

public class CongestionTaxApplicationException : Exception
{
    public CongestionTaxApplicationException(Exception ex, ICongestionTaxErrorHandler congestionTaxErrorHandler) :
        base(ex.Message, ex)
    {
        JsonErrorResponse = congestionTaxErrorHandler.GetError(ex);
    }
    public JsonErrorResponse JsonErrorResponse { get; private set; }
}