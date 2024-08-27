using System;
using System.IdentityModel.Tokens.Jwt;
using Application.Authentication;
using Azure;
using Core.Features.Queries.Authentications;
using Core.Features.Queries.Authentications.RefreshToken;
using Core.Features.Queries.GetListUsers;
using Core.Features.Queries.PostRoles;
using Core.Features.Queries.PostUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/auth/")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediadtor)
        {
            _mediator = mediadtor;
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody] PostLoginQuery request, CancellationToken cancellation)
        {
            var response = await _mediator.Send(request, cancellation);

            if (response is null) return BadRequest();

            response.Data.Token = JwtExtension.Generate(response.Data);
            return Ok(response);
        }

        [HttpPost("refresh")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery request, CancellationToken cancellation)
        {
            var response = await _mediator.Send(request, cancellation);

            if (response is null) return BadRequest();

            response.Data.Token = JwtExtension.Generate(response.Data);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] PostUsersQuery request, CancellationToken cancellation)
        {
            var response = await _mediator.Send(request, cancellation);

            if (response is null) return BadRequest();

            return Ok(response);
        }

        [HttpPost("role")]
        public async Task<PostRolesResponse> CreateRole(PostRolesQuery request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpGet("users")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ListUsers(GetListUsersQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> Logout([FromBody] PostLogoutQuery request, CancellationToken cancellation)
        {
            var response = await _mediator.Send(request, cancellation);
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            new JwtTokenLifetimeManager().SignOut(new JwtSecurityToken(_bearer_token));
            //Response.Cookies.Delete("token");
            if (response is null) return BadRequest();

            return Ok(response);
        }
    }
}

