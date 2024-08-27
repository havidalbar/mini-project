using System;
using Persistence.Models;

namespace Persistence.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<User?> GetUserByRefreshCode(Guid refreshToken, CancellationToken cancellationToken);
        Task<bool> AnyAsync(string email, CancellationToken cancelationToken);
        Task<List<User>> GetUsersWithRolesAsync();
        Task LogoutAsync(Guid userId, CancellationToken cancellationToken);
    }
}

