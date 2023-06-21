
namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public abstract class Entity : BaseEntity<string>, IEntity, IAuditLog
{
  private int? _requestedHashCode;
  private List<INotification> _domainEvents;
  public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }
  
  public void SetUpdatedAt(DateTimeOffset updatedAt)
  {
    UpdatedAt = updatedAt;
  }

  public Entity()
  {
    UpdatedAt = CreatedAt = DateTime.Now;
    _domainEvents = new List<INotification>();
  }

  public void AddDomainEvent(INotification eventItem)
  {
    _domainEvents ??= new List<INotification>();
    _domainEvents.Add(eventItem);
  }

  public void RemoveDomainEvent(INotification eventItem)
  {
    _domainEvents?.Remove(eventItem);
  }

  public void ClearDomainEvents()
  {
    _domainEvents?.Clear();
  }

  public bool IsTransient()
  {
    return Id == default(string);
  }

  public override bool Equals(object obj)
  {
    if (obj == null || !(obj is Entity))
      return false;

    if (Object.ReferenceEquals(this, obj))
      return true;

    if (this.GetType() != obj.GetType())
      return false;

    Entity item = (Entity)obj;

    if (item.IsTransient() || this.IsTransient())
      return false;
    else
      return item.Id == this.Id;
  }

  public override int GetHashCode()
  {
    if (!IsTransient())
    {
      // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
      if (!_requestedHashCode.HasValue)
        _requestedHashCode = Id.GetHashCode() ^ 31;
      
      return _requestedHashCode.Value;
    }
    else
      return base.GetHashCode();
  }
  
  public static bool operator ==(Entity left, Entity right)
  {
    if (Object.Equals(left, null))
      return (Object.Equals(right, null)) ? true : false;
    else
      return left.Equals(right);
  }

  public static bool operator !=(Entity left, Entity right)
  {
    return !(left == right);
  }
  
  public virtual void Commit(bool isRaiseEventInChangedData = false)
  {
    if (isRaiseEventInChangedData)
    {
      AddDomainEvent(new EntityChangedDomainEvent(this));
    }
  }
}
