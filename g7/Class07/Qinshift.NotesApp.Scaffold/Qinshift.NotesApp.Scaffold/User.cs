using System;
using System.Collections.Generic;

namespace Qinshift.NotesApp.Scaffold
{
    public partial class User
    {
        public User()
        {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = null!;

        public virtual ICollection<Note> Notes { get; set; }
    }
}
