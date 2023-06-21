
namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public interface IEntity
{
    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification eventItem);
    void ClearDomainEvents();
    bool Equals(object obj);
    int GetHashCode();
    bool IsTransient();
    void RemoveDomainEvent(INotification eventItem);
    void Commit(bool isRaiseEventInChangedData=false);
}
