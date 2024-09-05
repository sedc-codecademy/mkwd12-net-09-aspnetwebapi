using NotesAndTAgsApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DTOs.Notes
{
    public class NoteDto
    {
        public string Text { get; set; }

        public PriorityEnum Priority { get; set; }

        public TagEnum Tag { get; set; }

        public string UserFullName { get; set; }
    }
}
