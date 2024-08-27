using System;
using Persistence.Models;

namespace Core.Features.Queries.PostTodoDetails
{
	public class PostTodoDetailsResponse
	{
        public Guid TodoDetailId { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
        public string DetailNote { get; set; }
        public Guid TodoId { get; set; }

        public PostTodoDetailsResponse(TodoDetail todo)
        {
            TodoId = todo.TodoId;
            TodoDetailId = todo.TodoDetailId;
            Activity = todo.Activity;
            Category = todo.Category;
            DetailNote = todo.DetailNote;
        }
    }
}

