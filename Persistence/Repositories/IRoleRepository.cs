using System;
using Persistence.Models;

namespace Persistence.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRoles(List<Guid> ids);
    }
}

