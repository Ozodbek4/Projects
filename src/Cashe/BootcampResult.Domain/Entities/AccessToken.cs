using BootcampResult.Domain.Common.Entities;

namespace BootcampResult.Domain.Entities;

public class AccessToken : AuditableEntity
{
    public Guid UserId { get; set; }

    public bool IsRevoked { get; set; }
}