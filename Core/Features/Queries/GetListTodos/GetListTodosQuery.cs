using System;
using MediatR;

namespace Core.Features.Queries.GetListTodos
{
	public class GetListTodosQuery : IRequest<Object>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

    }
}

