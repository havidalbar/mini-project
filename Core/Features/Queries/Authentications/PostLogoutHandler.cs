using System;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.Authentications
{
    public class PostLogoutHandler : IRequestHandler<PostLogoutQuery, Response>
    {
        private readonly IUserRepository _repository;


        public PostLogoutHandler(IUserRepository repository)
		{
            _repository = repository;
        }

        public async Task<Response> Handle(PostLogoutQuery query, CancellationToken cancellationToken)
        {
            User? user = await _repository.GetUserByEmailAsync(query.Email, cancellationToken);

            await _repository.LogoutAsync(user.Id, cancellationToken);
            return new Response("succes logout", 200);
        }
    }
}

