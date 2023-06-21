namespace Fintranet.BuildingBlocks.Common.Domain.SeedWork;

public interface IAuditLog
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}