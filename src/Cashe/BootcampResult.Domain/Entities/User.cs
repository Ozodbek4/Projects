using BootcampResult.Domain.Common.Entities;

namespace BootcampResult.Domain.Entities;

public class User : SoftDeletedEntity, IModificationAuditableEntity, IDeletionAuditableEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    
    public byte Age { get; set; } 

    public string EmailAddress { get; set; } = default!;

    public Guid? ModifiedByUserId { get; set; }
    
    public Guid? DeletedByUserId { get; set; }
}