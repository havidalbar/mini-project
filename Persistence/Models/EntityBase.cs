using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }

        protected EntityBase() => Id = Guid.NewGuid();
    }
}

