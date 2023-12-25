using System.Linq.Expressions;
using BootcampResult.Domain.Common.Cashing;
using BootcampResult.Domain.Entities;
using BootcampResult.Persistence.Cashing.Brokers;
using BootcampResult.Persistence.DataContext;
using BootcampResult.Persistence.Repositories.Interfaces;

namespace BootcampResult.Persistence.Repositories;

public class UserCredentialsRepository(IdentityDbContext identityDbContext, ICasheBroker casheBroker)
    : EntityRepositoryBase<UserCredentials, IdentityDbContext>(identityDbContext, casheBroker, new CasheEntryOptions()),
        IUserCredentialsRepository
{
    public ValueTask<IEnumerable<UserCredentials>> GetAllAsync(bool asNoTracking = false) =>
        new (base.Get(asNoTracking: true).ToList());

    public new IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>>? predicate = default,
        bool asNoTracking = false) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<UserCredentials?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<IList<UserCredentials>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<UserCredentials> CreateAsync(UserCredentials user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.CreateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<UserCredentials> UpdateAsync(UserCredentials user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.UpdateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<UserCredentials> DeleteAsync(UserCredentials user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteAsync(user, saveChanges, cancellationToken);

    public new ValueTask<UserCredentials> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}