namespace BootcampResult.Domain.Common.Entities;

public interface ICreationAuditableEntity
{
    Guid CreatedByUserId { get; set; }
}