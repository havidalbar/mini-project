using System;
using Persistence.Models;

namespace Persistence.Repositories
{
	public interface ITodoDetailRepository : IGenericRepository<TodoDetail>
    {
        List<TodoDetail> GetTodoDetailsByTodoId(Guid todoId);
    }
}

