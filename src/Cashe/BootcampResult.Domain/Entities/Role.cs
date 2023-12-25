using BootcampResult.Domain.Common.Entities;
using BootcampResult.Domain.Enums;

namespace BootcampResult.Domain.Entities;

public class Role : AuditableEntity, IModificationAuditableEntity
{
    public RoleType Type { get; set; }

    public bool IsDisabled { get; set; }

    public Guid? ModifiedByUserId { get; set; }
}