using BootcampResult.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BootcampResult.Persistence.Interceptors;

public class ModificationAuditableEntity : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var modificationEntity = eventData.Context!.ChangeTracker
            .Entries<IModificationAuditableEntity>().ToList();
        
        modificationEntity.ForEach(entity =>
        {
            if (entity.State == EntityState.Modified)
                entity.Property(nameof(IModificationAuditableEntity.ModifiedByUserId)).CurrentValue = Guid.Empty;
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}