
namespace Fintranet.BuildingBlocks.Common.Application.Models.Command;

public class BaseCommand<TResponse> : IRequest<TResponse> , IBaseCommand
{
}

public class BaseCommand : IRequest, IBaseCommand
{
}
public interface IBaseCommand
{
}
