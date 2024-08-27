using System;
using MediatR;

namespace Core.Features.Queries.PostListTodoDetails
{
	public class PostListTodoDetailsQuery : IRequest<Object>
    {
        public List<TodoDetailEntity> TodoDetails { get; set; }

    }

    public class TodoDetailEntity
	{
        public Guid TodoId { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
        public string DetailNote { get; set; }
    }
}

