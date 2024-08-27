using System;
using Core.Features.Queries.PostUsers;
using MediatR;

namespace Core.Features.Queries.DeleteTodos
{
	public class DeleteTodosQuery : IRequest<Response>
    {
        public Guid TodoId { get; set; }
    }
}

