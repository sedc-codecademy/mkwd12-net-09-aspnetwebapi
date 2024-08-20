using System;
using System.Collections.Generic;

namespace EFDatabaseFirstDemo.Domain.Models
{
    public partial class Status
    {
        public Status()
        {
            Todos = new HashSet<Todo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }
    }
}
