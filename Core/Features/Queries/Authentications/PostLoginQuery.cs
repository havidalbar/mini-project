using System;
using Core.Features.Queries.PostUsers;
using MediatR;

namespace Core.Features.Queries.Authentications
{
	public class PostLoginQuery : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

