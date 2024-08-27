using System;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories
{
	public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {

        public TodoRepository(TableContext context) : base(context)
        {

        }
    }
}

