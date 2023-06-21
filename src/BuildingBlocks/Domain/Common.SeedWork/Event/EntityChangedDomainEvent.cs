namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork.Event;

public class EntityChangedDomainEvent  : INotification
{
    public EntityChangedDomainEvent(IEntity Entity)
    {
        this.Entity = Entity;
    }

  public IEntity Entity { get;private set; }
}
