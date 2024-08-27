using System;
using Core.Features.Queries.PostUsers;
using MediatR;

namespace Core.Features.Queries.Authentications.RefreshToken
{
	public class RefreshTokenQuery : IRequest<Response>
	{
		public Guid RefreshToken { get; set; }

    }
}

