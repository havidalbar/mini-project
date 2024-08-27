using System;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.DeleteTodos
{
	public class DeleteTodosHandler : IRequestHandler<DeleteTodosQuery, Response>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public DeleteTodosHandler(ITodoRepository todoRepository, ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
        {
            _todoRepository = todoRepository;
            _todoDetailRepository = todoDetailRepository;
            _cacheService = cacheService;
        }

        public async Task<Response> Handle(DeleteTodosQuery query, CancellationToken cancellationToken)
        {
            List<Todo>? todos = _cacheService.Get<Todo>("todos");
            Todo? todo;
            if (todos is null && _cacheService.CheckActive())
            {
                todo = _todoRepository.GetById(query.TodoId);
                if (todo is null)
                    return new Response("todo not found", 404);
                await _todoRepository.Delete(todo);
                await _todoDetailRepository.DeleteBatch(_todoDetailRepository.GetTodoDetailsByTodoId(todo.TodoId));
                _cacheService.Remove("todos");
                _cacheService.Add("todos", _todoRepository.GetAll());
            }
            todo = _todoRepository.GetById(query.TodoId);
            if (todo is null)
                return new Response("todo not found", 404);
            await _todoRepository.Delete(todo);
            return new Response("success", 200);
        }
    }
}

