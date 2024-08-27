using NotesApp.Domain.Models;
using NotesApp.Dto.NoteDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto()
            {
                Id = note.Id,
                Text = note.Text,
                Tag = note.Tag,
                Priority = note.Priority,
                UserFullName = $"{note.User.FirstName} {note.User.LastName}"
            };
        }

        public static Note ToNote(this AddNoteDto addNoteDto)
        {
            return new Note()
            {
                Text = addNoteDto.Text,
                Tag = addNoteDto.Tag,
                Priority = addNoteDto.Priority,
                UserId = addNoteDto.UserId //FK
            };
        }
    }
}
