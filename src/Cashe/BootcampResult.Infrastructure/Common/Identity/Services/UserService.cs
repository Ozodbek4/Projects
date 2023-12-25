using System.Linq.Expressions;
using BootcampResult.Application.Common.Identity.Services;
using BootcampResult.Domain.Entities;
using BootcampResult.Persistence.Repositories.Interfaces;

namespace BootcampResult.Infrastructure.Common.Identity.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public ValueTask<IEnumerable<User>> GetAllAsync(bool asNoTracking = false) =>
        userRepository.GetAllAsync(asNoTracking);

    public IEnumerable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false) =>
        userRepository.Get(predicate, asNoTracking);

    public ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        userRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<User> CreateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        user.Id = Guid.NewGuid();
        
        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userRepository.UpdateAsync(user, saveChanges, cancellationToken);

    public ValueTask<User> DeleteAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userRepository.DeleteAsync(user, saveChanges, cancellationToken);

    public ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        userRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}