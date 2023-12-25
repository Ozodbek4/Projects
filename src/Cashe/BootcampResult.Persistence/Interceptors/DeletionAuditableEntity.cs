using BootcampResult.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BootcampResult.Persistence.Interceptors;

public class DeletionAuditableEntity : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var deletionEntity = eventData.Context!.ChangeTracker.Entries<IDeletionAuditableEntity>().ToList();
        
        deletionEntity.ForEach(entity =>
        {
            if (entity.State == EntityState.Deleted)
                entity.Property(nameof(IDeletionAuditableEntity.DeletedByUserId)).CurrentValue = Guid.Empty;
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}