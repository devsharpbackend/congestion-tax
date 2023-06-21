namespace Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler.Exceptions;


public class ConflictException : ApplicationException
{
    public ConflictException(string message)
        : base(message)
    {
    }
}
