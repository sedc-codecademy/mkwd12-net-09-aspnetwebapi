using System;
using System.Collections.Generic;

namespace Qinshift.NotesApp.Scaffold
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int Priority { get; set; }
        public int Tag { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
