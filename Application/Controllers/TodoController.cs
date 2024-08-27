using System;
using Core.Features.Queries.DeleteTodos;
using Core.Features.Queries.GetListTodos;
using Core.Features.Queries.PostListTodos;
using Core.Features.Queries.PostTodos;
using Core.Features.Queries.PutTodos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class TodoController : BaseController
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
		{
            _mediator = mediator;
        }

        [HttpGet("todo")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> ListTodos(GetListTodosQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("todo")]
        [Authorize(Roles = "ADMIN")]
        public async Task<PostTodosResponse> PostTodo(PostTodosQuery request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpPost("todo/all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostListTodo(PostListTodosQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("todo")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutTodo(PutTodosQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("todo")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteTodo(DeleteTodosQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

