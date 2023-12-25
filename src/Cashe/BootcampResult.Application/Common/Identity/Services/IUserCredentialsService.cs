using BootcampResult.Domain.Entities;
using System.Linq.Expressions;

namespace BootcampResult.Application.Common.Identity.Services;

public interface IUserCredentialsService
{
    ValueTask<IEnumerable<UserCredentials>> GetAllAsync(bool asNoTracking = false);

    IEnumerable<UserCredentials> Get(Expression<Func<UserCredentials, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<UserCredentials?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<IList<UserCredentials>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> CreateAsync(UserCredentials user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> UpdateAsync(UserCredentials user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> DeleteAsync(UserCredentials user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}