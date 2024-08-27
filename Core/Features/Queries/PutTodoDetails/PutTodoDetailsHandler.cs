using System;
using Core.Features.Queries.PostTodoDetails;
using Core.Features.Queries.PostUsers;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.PutTodoDetails
{
	public class PutTodoDetailsHandler : IRequestHandler<PutTodoDetailsQuery, Object>
    {
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public PutTodoDetailsHandler(ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
		{
            _todoDetailRepository = todoDetailRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Handle(PutTodoDetailsQuery query, CancellationToken cancellationToken)
        {
            List<TodoDetail>? todoDetails = _cacheService.Get<TodoDetail>("todoDetails");
            TodoDetail? details;
            if (todoDetails is null && _cacheService.CheckActive())
            {
                details = _todoDetailRepository.GetById(query.TodoDetailId);
                if (details is null)
                    return new Response("todo not found", 404);
                details.Activity = query.Activity;
                details.Category = query.Category;
                details.DetailNote = query.DetailNote;
                await _todoDetailRepository.Update(details);
                _cacheService.Remove("todoDetails");
                _cacheService.Add("todoDetails", _todoDetailRepository.GetAll());
                return new PostTodoDetailsResponse(details);
            }

            details = _todoDetailRepository.GetById(query.TodoDetailId);
            if (details is null)
                return new Response("todo not found", 404);
            details.Activity = query.Activity;
            details.Category = query.Category;
            details.DetailNote = query.DetailNote;
            await _todoDetailRepository.Update(details);
            return new PostTodoDetailsResponse(details);

        }
    }
}

