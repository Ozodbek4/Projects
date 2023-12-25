using BootcampResult.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BootcampResult.Persistence.Interceptors;

public class CreationAuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var creationEntity = eventData.Context!.ChangeTracker.Entries<ICreationAuditableEntity>().ToList();
        
        creationEntity.ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
                entry.Property(nameof(ICreationAuditableEntity.CreatedByUserId)).CurrentValue = Guid.Empty;
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}