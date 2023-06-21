namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public interface IFullAuditLog<T> : IAuditLog
{
    public T CreateUserId { get;  set; }
    public T UpdateUserId { get; set; }
}
    