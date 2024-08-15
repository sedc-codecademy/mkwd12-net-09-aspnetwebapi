using NotesApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Domain.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get;set; }
        public User User { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
