namespace Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
