using System;
using AutoMapper;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.Authentications.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RefreshTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            // Search user
            User? user;
            try
            {
                // Search user role
                user = await _userRepository.GetUserByRefreshCode(request.RefreshToken, cancellationToken);
                if (user is null)
                {
                    return new Response("User not found", 404);
                }
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }


            try
            {
                // Commit the chages in database
                user.GenerateRefreshToken();
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }

            // Mapper user to dto
            PostLoginResponse userDTO = new PostLoginResponse
            {
                Id = user.Id,
                Email = user.Email
            };
            List<RoleLoginResponse> roleResponse = user.Roles
            .Select(r => new RoleLoginResponse(r.Name))
            .ToList();
            userDTO.Roles = roleResponse;
            userDTO.RefreshToken = user.RefreshToken;
            return new Response("Token Refreshed", 200, userDTO);
        }
    }
}

