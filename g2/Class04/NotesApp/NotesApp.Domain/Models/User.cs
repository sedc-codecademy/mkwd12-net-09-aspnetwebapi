using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string Username { get;set; }
        public string Password { get;set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
