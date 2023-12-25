
namespace BootcampResult.Domain.Common.Entities;

public class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    public DateTime? DeletedTime { get; set; }

    public bool IsDeleted { get; set; }
}