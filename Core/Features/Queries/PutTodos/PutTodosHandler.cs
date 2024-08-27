using System;
using Core.Features.Queries.PostTodos;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PutTodos
{
	public class PutTodosHandler : IRequestHandler<PutTodosQuery, Object>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICacheService _cacheService;

        public PutTodosHandler(ITodoRepository todoRepository, ICacheService cacheService)
        {
            _todoRepository = todoRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Handle(PutTodosQuery query, CancellationToken cancellationToken)
        {
            List<Todo>? todos = _cacheService.Get<Todo>("todos");
            Todo? todo;
            if (todos is null && _cacheService.CheckActive())
            {
                todo = _todoRepository.GetById(query.TodoId);
                if (todo is null)
                    return new Response("todo not found", 404);
                todo.Day = query.Day;
                todo.Note = query.Note;
                await _todoRepository.Update(todo);
                _cacheService.Remove("todos");
                _cacheService.Add("todos", _todoRepository.GetAll());
                return new PostTodosResponse(todo);
            }
            todo = _todoRepository.GetById(query.TodoId);
            if (todo is null)
                return new Response("todo not found", 404);
            todo.Day = query.Day;
            todo.Note = query.Note;
            await _todoRepository.Update(todo);
            return new PostTodosResponse(todo);

        }
    }
}

