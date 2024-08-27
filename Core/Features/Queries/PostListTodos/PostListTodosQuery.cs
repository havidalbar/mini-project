using System;
using MediatR;

namespace Core.Features.Queries.PostListTodos
{
	public class PostListTodosQuery : IRequest<Object>
    {
        public List<TodoEntity> Todos { get; set; }
    }

    public class TodoEntity
    {
        public string Day { get; set; }
        public string Note { get; set; }
    }
}

