using System;
using MediatR;
using Persistence.Models;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.GetListTodoDetails
{
	public class GetListTodoDetailsHandler : IRequestHandler<GetListTodoDetailsQuery, Object>
    {
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly ICacheService _cacheService;

        public GetListTodoDetailsHandler(ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
        {
            _todoDetailRepository = todoDetailRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Handle(GetListTodoDetailsQuery query, CancellationToken cancellationToken)
        {
            List<TodoDetail>? todoDetails = _cacheService.Get<TodoDetail>("todoDetails");
            if (todoDetails is null)
            {
                if (_cacheService.CheckActive())
                {
                    List<TodoDetail> todoDetailEntities = _todoDetailRepository.GetAll();
                    _cacheService.Add("todoDetails", todoDetailEntities);
                }
            }

            todoDetails = await _todoDetailRepository.GetPaged(query.pageNumber, query.pageSize);

            if (todoDetails is null)
                return new List<TodoDetail>();

            return todoDetails;
        }
    }
}

