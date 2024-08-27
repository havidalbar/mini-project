using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("Roles")]
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();

        public Role()
        { }

        public Role(string name)
        {
            Name = name;
        }
    }
}

