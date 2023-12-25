namespace BootcampResult.Domain.Common.Entities;

public interface ISoftDeletedEntity : IAuditableEntity
{
    DateTime? DeletedTime { get; set; }

    bool IsDeleted { get; set; }
}