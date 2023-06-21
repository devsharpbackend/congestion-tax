namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;


public interface IRepository<T>  
{
    IUnitOfWork UnitOfWork { get; }
}
