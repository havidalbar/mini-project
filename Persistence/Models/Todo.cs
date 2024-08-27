using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("Todo")]
    public class Todo
	{
        [Key]
        [Required]
        public Guid TodoId { get; set; }
        public string Day { get; set; }
        public DateTime TodayDate { get; set; }
        public string Note { get; set; }
        public int DetailCount { get; set; }
        public List<TodoDetail> todoDetails { get; set; }
    }
}

