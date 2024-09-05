using System;
using Core.Features.Queries.DeleteTodoDetails;
using Core.Features.Queries.GetListTodoDetails;
using Core.Features.Queries.PostListTodoDetails;
using Core.Features.Queries.PostTodoDetails;
using Core.Features.Queries.PutTodoDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/todo/")]
    public class TodoDetailController : BaseController
    {
        private readonly IMediator _mediator;

        public TodoDetailController(IMediator mediator)
		{
            _mediator = mediator;
        }

        [HttpGet("detail")]
        // [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> ListTodoDetails([FromQuery] GetListTodoDetailsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("detail")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostTodoDetails(PostTodoDetailsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("detail/all")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostListTodoDetails(PostListTodoDetailsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("detail")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutTodoDetails(PutTodoDetailsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("detail")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteTodoDetails([FromQuery] DeleteTodoDetailsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

