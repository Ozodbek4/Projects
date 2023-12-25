using System.Linq.Expressions;
using BootcampResult.Domain.Common.Cashing;
using BootcampResult.Domain.Entities;
using BootcampResult.Persistence.Cashing.Brokers;
using BootcampResult.Persistence.DataContext;
using BootcampResult.Persistence.Repositories.Interfaces;

namespace BootcampResult.Persistence.Repositories;

public class UserRepository(IdentityDbContext dbContext, ICasheBroker casheBroker)
    : EntityRepositoryBase<User, IdentityDbContext>(
            dbContext,
            casheBroker,
            new CasheEntryOptions())
        , IUserRepository
{
    public ValueTask<IEnumerable<User>> GetAllAsync(bool asNoTracking = false) =>
        new(base.Get(asNoTracking: asNoTracking));

    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<User> CreateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.CreateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.UpdateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User> DeleteAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}