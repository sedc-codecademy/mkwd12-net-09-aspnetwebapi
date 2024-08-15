using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Dto.NotesDto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public string User { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
