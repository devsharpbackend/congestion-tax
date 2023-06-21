namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public class BaseEntity<TKey>
{
    private TKey _id;
    public TKey Id
    {
        get => _id;
        protected set => _id = value;
    }
}