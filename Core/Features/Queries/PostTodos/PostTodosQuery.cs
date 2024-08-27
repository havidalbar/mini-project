using System;
using MediatR;

namespace Core.Features.Queries.PostTodos
{
	public class PostTodosQuery : IRequest<PostTodosResponse>
    {
        public string Day { get; set; }
        public string Note { get; set; }
    }
}

