using System;
using MediatR;

namespace Core.Features.Queries.PostTodoDetails
{
	public class PostTodoDetailsQuery : IRequest<Object>
    {
        public Guid TodoId { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
        public string DetailNote { get; set; }
    }
}

