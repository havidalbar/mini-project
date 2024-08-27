using System;
using MediatR;

namespace Core.Features.Queries.PutTodos
{
	public class PutTodosQuery : IRequest<Object>
    {
        public Guid TodoId { get; set; }
        public string Day { get; set; }
        public string Note { get; set; }
    }
}

