namespace Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler;


public interface IErrorHandler
{
    JsonErrorResponse GetError(Exception ex);
}
