using System;
using AutoMapper;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Authentication;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.Authentications
{
    public class PostLoginHandler : IRequestHandler<PostLoginQuery, Response>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHashingService _service;

        public PostLoginHandler(IUserRepository repository, IMapper mapper, IPasswordHashingService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response> Handle(PostLoginQuery request, CancellationToken cancellationToken)
        {
            User? user;
            try
            {
                // Search user in database
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
                if (user is null)
                    return new Response("User not found", 404);
            }
            catch
            {
                return new Response("Internal Server Error", 500);
            }

            // Validate user password
            bool isVerified = _service.VerifyHashedPassword(user.Password, request.Password);
            if (!isVerified)
            {
                return new Response("Password dont match", 404);
            }

            // Mapper user to DTO
            PostLoginResponse userDTO = _mapper.Map<PostLoginResponse>(user);
            return new Response("User authenticated", 200, userDTO);
        }
    }
}

