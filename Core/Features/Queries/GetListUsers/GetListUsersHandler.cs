using System;
using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.GetListUsers
{
	public class GetListUsersHandler : IRequestHandler<GetListUsersQuery, Object>
    {
        private readonly IUserRepository _userRepository;

        public GetListUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Object> Handle(GetListUsersQuery query, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepository.GetUsersWithRolesAsync();
            if (users is null)
                return new List<User>();
            return users;

        }
    }
}

