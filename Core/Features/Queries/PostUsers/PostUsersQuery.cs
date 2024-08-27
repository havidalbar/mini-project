using System;
using MediatR;

namespace Core.Features.Queries.PostUsers
{
	public class PostUsersQuery : IRequest<Response>
	{
		public string Email { get; set; }
		public string Password { get; set; }
        public List<Guid> RoleIds { get; set; }
	}
}

