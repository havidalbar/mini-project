using System;
using Core.Features.Queries.PostUsers;
using MediatR;

namespace Core.Features.Queries.DeleteTodoDetails
{
	public class DeleteTodoDetailsQuery : IRequest<Response>
    {
        public Guid TodoDetailId { get; set; }

    }
}

