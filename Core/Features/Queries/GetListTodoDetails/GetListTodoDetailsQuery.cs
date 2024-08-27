using System;
using MediatR;

namespace Core.Features.Queries.GetListTodoDetails
{
	public class GetListTodoDetailsQuery : IRequest<Object>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}

