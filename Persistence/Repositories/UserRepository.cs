using System;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TableContext _context;

        public UserRepository(TableContext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> AnyAsync(string email, CancellationToken cancelationToken)
        {
            return _context.Users
                        .AnyAsync(x => x.Email == email, cancelationToken);
        }

        public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _context.Users
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public Task<User?> GetUserByRefreshCode(Guid refreshToken, CancellationToken cancellationToken)
        {
            return _context.Users
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken: cancellationToken);
        }

        public Task<List<User>> GetUsersWithRolesAsync()
        {
            return _context.Users
                .Include(u => u.Roles)
                .Select(u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = u.Roles.Select(r => new Role(r.Name)).ToList()
                })
                .ToListAsync();
        }

        public async Task LogoutAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);

            if (user != null)
            {
                user.RefreshToken = Guid.Empty;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

