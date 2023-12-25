using BootcampResult.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BootcampResult.Persistence.Interceptors;

public class SoftDeletedInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var softDeletedEntity = eventData.Context!.ChangeTracker.Entries<ISoftDeletedEntity>().ToList();
        
        softDeletedEntity.ForEach(entry =>
        {
            if (entry.State != EntityState.Deleted)
                return;

            entry.Property(nameof(ISoftDeletedEntity.DeletedTime)).CurrentValue = DateTime.UtcNow;
            entry.Property(nameof(ISoftDeletedEntity.IsDeleted)).CurrentValue = true;
            entry.State = EntityState.Modified;
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}