using System.Linq.Expressions;
using BootcampResult.Application.Common.Identity.Services;
using BootcampResult.Domain.Entities;
using BootcampResult.Persistence.Repositories.Interfaces;

namespace BootcampResult.Infrastructure.Common.Identity.Services;

public class UserCredentialsService(IUserCredentialsRepository userCredentialsRepository) : IUserCredentialsService
{
    public ValueTask<IEnumerable<UserCredentials>> GetAllAsync(bool asNoTracking = false) =>
        userCredentialsRepository.GetAllAsync(asNoTracking);

    public IEnumerable<UserCredentials> Get(Expression<Func<UserCredentials, bool>>? predicate = default,
        bool asNoTracking = false) =>
        userCredentialsRepository.Get(predicate, asNoTracking);

    public ValueTask<UserCredentials?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<UserCredentials>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.CreateAsync(userCredentials, saveChanges, cancellationToken);

    public ValueTask<UserCredentials> UpdateAsync(UserCredentials userCredentials, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.UpdateAsync(userCredentials, saveChanges, cancellationToken);

    public ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.DeleteAsync(userCredentials, saveChanges, cancellationToken);

    public ValueTask<UserCredentials> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userCredentialsRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}