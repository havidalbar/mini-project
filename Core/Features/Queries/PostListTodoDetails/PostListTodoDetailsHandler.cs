using System;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PostListTodoDetails
{
	public class PostListTodoDetailsHandler : IRequestHandler<PostListTodoDetailsQuery, Object>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public PostListTodoDetailsHandler(ITodoDetailRepository todoDetailRepository, ITodoRepository todoRepository, ICacheService cacheService)
		{
            _todoDetailRepository = todoDetailRepository;
            _todoRepository = todoRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Handle(PostListTodoDetailsQuery query, CancellationToken cancellationToken)
        {
            List<TodoDetail> todoDetails = new List<TodoDetail>();
            foreach (TodoDetailEntity entity in query.TodoDetails)
            {
                if (Enum.GetNames(typeof(CategoryEnum)).ToList().Contains(entity.Category))
                {
                    Todo? todo = _todoRepository.GetById(entity.TodoId);
                    if (todo is not null)
                    {
                        var newTodoDetail = new TodoDetail()
                        {
                            TodoDetailId = new Guid(),
                            TodoId = todo.TodoId,
                            Activity = entity.Activity,
                            Category = entity.Category,
                            DetailNote = entity.DetailNote
                        };
                        todoDetails.Add(newTodoDetail);
                        todo.DetailCount += 1;
                        todo.todoDetails = _todoDetailRepository.GetTodoDetailsByTodoId(todo.TodoId);
                        todoDetails.Add(newTodoDetail);
                        await _todoRepository.Update(todo);
                    }
                }
            }
            await _todoDetailRepository.CreateBatch(todoDetails);

            if (_cacheService.CheckActive())
            {
                List<Todo> todos = _todoRepository.GetAll();
                List<TodoDetail> todosDetails = _todoDetailRepository.GetAll();
                _cacheService.Remove("todos");
                _cacheService.Add("todos", todos);
                _cacheService.Remove("todoDetails");
                _cacheService.Add("todoDetails", todosDetails);
            }
            todoDetails = _todoDetailRepository.GetAll();
            return todoDetails;
        }
	}
}

