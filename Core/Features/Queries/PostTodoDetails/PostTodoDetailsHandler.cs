using System;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PostTodoDetails
{
	public class PostTodoDetailsHandler : IRequestHandler<PostTodoDetailsQuery, Object>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public PostTodoDetailsHandler(ITodoDetailRepository todoDetailRepository, ITodoRepository todoRepository, ICacheService cacheService)
		{
			_todoDetailRepository = todoDetailRepository;
			_todoRepository = todoRepository;
			_cacheService = cacheService;
		}

		public async Task<Object> Handle(PostTodoDetailsQuery query, CancellationToken cancellationToken)
		{
            //if (!Enum.GetValues<CategoryEnum>().ToList().Contains(query.Category))
            if(!Enum.GetNames(typeof(CategoryEnum)).ToList().Contains(query.Category))
            {
                return new Response("category todo not found, category enum are [DailyActivity|Task]", 400);
            }

			Todo? todo = _todoRepository.GetById(query.TodoId);
			if (todo is null)
			{
				return new Response("todo not found", 404);
			}

            var newTodoDetail = new TodoDetail()
            {
                TodoDetailId = new Guid(),
                TodoId = todo.TodoId,
                Activity = query.Activity,
                Category = query.Category,
                DetailNote = query.DetailNote
            };

            var todoDetail = await _todoDetailRepository.Create(newTodoDetail);
            todo.DetailCount += 1;
            todo.todoDetails.Add(newTodoDetail);
            await _todoRepository.Update(todo);

            if (_cacheService.CheckActive())
            {
                List<Todo> todos = _todoRepository.GetAll();
                List<TodoDetail> todoDetails = _todoDetailRepository.GetAll();
                _cacheService.Remove("todos");
                _cacheService.Add("todos", todos);
                _cacheService.Remove("todoDetails");
                _cacheService.Add("todoDetails", todoDetails);
            }

            return new PostTodoDetailsResponse(todoDetail);
        }

    }
}

