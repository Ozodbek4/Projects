using BootcampResult.Domain.Common.Entities;

namespace BootcampResult.Domain.Entities;

public class UserCredentials : AuditableEntity
{
    public Guid UserId { get; set; }

    public string HashedPassword { get; set; } = default!;
}