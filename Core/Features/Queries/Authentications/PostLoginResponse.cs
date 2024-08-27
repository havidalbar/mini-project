using System;
using Persistence.Models;

namespace Core.Features.Queries.Authentications
{
	public class PostLoginResponse
	{
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<RoleLoginResponse> Roles { get; set; }
        public string? Token { get; set; }
        public Guid? RefreshToken { get; set; }
    }
}

