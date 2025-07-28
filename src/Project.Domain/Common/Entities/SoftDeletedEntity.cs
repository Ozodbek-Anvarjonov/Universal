namespace Project.Domain.Common.Entities;

public class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    public long? DeletedById { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}