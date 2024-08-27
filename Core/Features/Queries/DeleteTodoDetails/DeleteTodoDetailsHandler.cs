using System;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.DeleteTodoDetails
{
	public class DeleteTodoDetailsHandler : IRequestHandler<DeleteTodoDetailsQuery, Response>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public DeleteTodoDetailsHandler(ITodoRepository todoRepository, ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
        {
            _todoRepository = todoRepository;
            _todoDetailRepository = todoDetailRepository;
            _cacheService = cacheService;
        }

        public async Task<Response> Handle(DeleteTodoDetailsQuery query, CancellationToken cancellationToken)
        {
            List<TodoDetail>? todoDetails = _cacheService.Get<TodoDetail>("todoDetails");
            TodoDetail? details;
            if (todoDetails is null && _cacheService.CheckActive())
            {
                details = _todoDetailRepository.GetById(query.TodoDetailId);
                if (details is null)
                    return new Response("todo not found", 404);
                await _todoDetailRepository.Delete(details);
                _cacheService.Remove("todoDetails");
                _cacheService.Add("todoDetails", _todoDetailRepository.GetAll());
            }

            details = _todoDetailRepository.GetById(query.TodoDetailId);
            if (details is null)
                return new Response("tododetails not found", 404);
            await _todoDetailRepository.Delete(details);
            return new Response("success", 200);
        }
	}
}

