using System;
using Core.Features.Queries.Authentications;
using MediatR;
using Persistence.Authentication;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.PostUsers
{
    public class CreateUserHandler : IRequestHandler<PostUsersQuery, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHashingService _service;

        public CreateUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHashingService service)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _service = service;
        }

        public async Task<Response> Handle(PostUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if email is avaliable
                bool isAvaliable = await _userRepository.AnyAsync(request.Email, cancellationToken);
                if (isAvaliable)
                {
                    return new Response("Email already in use", 404);
                }
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }

            // Get roles
            List<Role> roles = new List<Role>();

            try
            {
                roles = await _roleRepository.GetRoles(request.RoleIds);
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }


            // Generate User object
            User user = new User(request.Email, _service.HashPassword(request.Password));
            user.Roles = roles;

            try
            {
                // Save user in database
                var usr = await _userRepository.Create(user);
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }
            var response = new PostLoginResponse();
            response.Id = user.Id;
            response.Email = user.Email;
            response.Roles = user.Roles.Select(u => new RoleLoginResponse(u.Name)).ToList();
            response.Token = user.RefreshToken.ToString();
            response.RefreshToken = user.RefreshToken;
            return new Response("User created", 201, response);
        }
    }
}

