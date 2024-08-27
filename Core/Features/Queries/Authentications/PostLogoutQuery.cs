using System;
using Core.Features.Queries.PostUsers;
using MediatR;

namespace Core.Features.Queries.Authentications
{
	public class PostLogoutQuery : IRequest<Response>
	{
        public string Email { get; set; }
    }
}

