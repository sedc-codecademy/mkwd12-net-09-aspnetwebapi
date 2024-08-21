using System;
using System.Collections.Generic;

namespace EFDatabaseFirstDemo.Domain.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
