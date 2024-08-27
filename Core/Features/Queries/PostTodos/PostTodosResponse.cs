using System;
using Persistence.Models;

namespace Core.Features.Queries.PostTodos
{
    public class PostTodosResponse
    {
        public Guid TodoId { get; set; }
        public string Day { get; set; }
        public DateTime TodayDate { get; set; }
        public string Note { get; set; }
        public int DetailCount { get; set; }
        public List<TodoDetail> todoDetails { get; set; }

        public PostTodosResponse(Todo todo)
        {
            TodoId = todo.TodoId;
            Day = todo.Day;
            TodayDate = todo.TodayDate;
            Note = todo.Note;
            DetailCount = todo.DetailCount;
            todoDetails = new List<TodoDetail>();
        }
    }
}

