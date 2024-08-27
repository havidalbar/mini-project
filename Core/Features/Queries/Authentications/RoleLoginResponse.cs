using System;
namespace Core.Features.Queries.Authentications
{
	public class RoleLoginResponse
	{
        public string Name { get; set; }

        public RoleLoginResponse(string name)
        {
            Name = name;
        }
    }
}

