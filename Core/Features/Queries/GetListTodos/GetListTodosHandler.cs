using System;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.GetListTodos
{
	public class GetListTodosHandler : IRequestHandler<GetListTodosQuery, Object>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public GetListTodosHandler(ITodoRepository todoRepository, ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
		{
            _todoRepository = todoRepository;
            _todoDetailRepository = todoDetailRepository;
            _cacheService = cacheService;
		}

        public async Task<Object> Handle(GetListTodosQuery query, CancellationToken cancellationToken)
        {
            List<Todo>? todos = _cacheService.Get<Todo>("todos");
            if (todos is null)
            {
                if (_cacheService.CheckActive())
                {
                    List<Todo> todoEntities = _todoRepository.GetAll();
                    _cacheService.Add("todos", todoEntities);
                }
                todos = await _todoRepository.GetPaged(query.pageNumber, query.pageSize);
            }
            todos = await _todoRepository.GetPaged(query.pageNumber, query.pageSize);
            if (todos is null)
                return new List<Todo>();

            todos.ForEach(e => e.todoDetails = _todoDetailRepository.GetTodoDetailsByTodoId(e.TodoId));

            return todos;

        }
	}
}

