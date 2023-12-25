
namespace BootcampResult.Domain.Common.Entities;

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }

    public DateTime? ModifiedTime { get; set; }
}