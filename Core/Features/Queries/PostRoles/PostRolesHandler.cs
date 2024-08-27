using System;
using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.PostRoles
{
	public class PostRolesHandler : IRequestHandler<PostRolesQuery, PostRolesResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public PostRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<PostRolesResponse> Handle(PostRolesQuery request, CancellationToken cancellationToken)
        {
            var newRole = new Role()
            {
                Name = request.Name
            };

            Role role = await _roleRepository.Create(newRole);
            var response = new PostRolesResponse()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return response;
        }
    }
}

