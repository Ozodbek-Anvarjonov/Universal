using Project.Domain.Entities;

namespace Project.Domain.Common.Entities;

public interface ISoftDeletedEntity : IAuditableEntity
{
    long? DeletedById { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
}