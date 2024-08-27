using System;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PostTodos
{
	public class PostTodosHandler : IRequestHandler<PostTodosQuery, PostTodosResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICacheService _cacheService;

        public PostTodosHandler(ITodoRepository todoRepository, ICacheService cacheService)
		{
            _todoRepository = todoRepository;
            _cacheService = cacheService;
		}

        public async Task<PostTodosResponse> Handle(PostTodosQuery query, CancellationToken cancellationToken)
        {
            var newTodo = new Todo()
            {
                TodoId = new Guid(),
                Day = query.Day,
                TodayDate = DateTime.Today,
                Note = query.Note,
                DetailCount = 0,
                todoDetails = new List<TodoDetail>()
            };

            var todo = await _todoRepository.Create(newTodo);

            if (_cacheService.CheckActive())
            {
                List<Todo> todos = _todoRepository.GetAll();
                _cacheService.Remove("todos");
                _cacheService.Add("todos", todos);
            }

            return new PostTodosResponse(todo);

        }
	}
}

