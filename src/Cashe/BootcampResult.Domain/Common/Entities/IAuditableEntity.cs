namespace BootcampResult.Domain.Common.Entities;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedTime { get; set; }

    DateTime? ModifiedTime { get; set; }
}