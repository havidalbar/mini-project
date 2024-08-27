using System;
using MediatR;

namespace Core.Features.Queries.PutTodoDetails
{
	public class PutTodoDetailsQuery : IRequest<Object>
	{
        public Guid TodoDetailId { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
        public string DetailNote { get; set; }
    }
}

