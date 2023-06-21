
namespace Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler;

public class JsonErrorResponse
{
    public object? Messages { get; set; }

    public object? DeveloperMessage { get; set; }

    public int StatusCode { get; set; } = 500;
}
