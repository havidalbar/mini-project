using System;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories
{
	public class TodoDetailRepository : GenericRepository<TodoDetail>, ITodoDetailRepository
    {
        private readonly TableContext _context;

        public TodoDetailRepository(TableContext context) : base(context)
        {
            _context = context;
        }

        public List<TodoDetail> GetTodoDetailsByTodoId(Guid todoId)
        {
            return _context.TodoDetails
                .Where(td => td.TodoId == todoId)
                .ToList();
        }
    }
}

