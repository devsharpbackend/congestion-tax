
namespace Fintranet.BuildingBlocks.Common.Application.Models.Command;

public class BaseEntityCommand<T>: IRequest<T>, IBaseCommand
{
    public virtual string Id { get; set; }
}

public class BaseEntityCommand<T,TKey> : IRequest<T>, IBaseCommand
{
    public virtual TKey Id { get; set; }
}

public class BaseEntityCommand : IRequest, IBaseCommand
{
    public virtual string Id { get; set; }
}
