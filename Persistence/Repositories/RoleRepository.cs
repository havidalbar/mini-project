using System;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly TableContext _context;
        public RoleRepository(TableContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRoles(List<Guid> ids)
        {
            return await _context.Roles.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}

