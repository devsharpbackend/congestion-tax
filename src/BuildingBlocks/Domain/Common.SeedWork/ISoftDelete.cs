namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public interface ISoftDelete
{
    public bool Deleted { set; get; }
}