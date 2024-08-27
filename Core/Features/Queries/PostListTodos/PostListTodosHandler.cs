using System;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PostListTodos
{
	public class PostListTodosHandler : IRequestHandler<PostListTodosQuery, Object>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICacheService _cacheService;

        public PostListTodosHandler(ITodoRepository todoRepository, ICacheService cacheService)
		{
            _todoRepository = todoRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Handle(PostListTodosQuery query, CancellationToken cancellationToken)
        {
            List<Todo> todos = new List<Todo>();
            foreach(TodoEntity t in query.Todos)
            {
                var newTodo = new Todo()
                {
                    TodoId = new Guid(),
                    Day = t.Day,
                    TodayDate = DateTime.Today,
                    Note = t.Note,
                    DetailCount = 0,
                    todoDetails = new List<TodoDetail>()
                };
                todos.Add(newTodo);
            }

            await _todoRepository.CreateBatch(todos);
            if (_cacheService.CheckActive())
            {
                List<Todo> todo = _todoRepository.GetAll();
                _cacheService.Remove("todos");
                _cacheService.Add("todos", todo);
            }
            return todos;
        }
    }
}

