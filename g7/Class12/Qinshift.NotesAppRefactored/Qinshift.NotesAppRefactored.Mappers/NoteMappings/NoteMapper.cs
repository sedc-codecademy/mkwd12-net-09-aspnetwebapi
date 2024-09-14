using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Dtos.NoteDtos;

namespace Qinshift.NotesAppRefactored.Mappers.NoteMappings
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Tag = note.Tag,
                priority = note.Priority,
                Text = note.Text,
                //UserFullName = note.User != null ? $"{note.User.FirstName} {note.User.LastName}" : "Unknown user"
                UserFullName = $"{note.User.FirstName} {note.User.LastName}"
            };
        } 

        public static Note ToNote(this AddNoteDto addNote)
        {
            return new Note()
            {
                Text = addNote.Text,
                Priority = addNote.Priority,
                Tag = addNote.Tag,
                UserId = addNote.UserId,
            };
        }

        public static Note ToNote(this UpdateNoteDto updateNoteDto, Note noteDb)
        {
            noteDb.Text = updateNoteDto.Text;
            //...
            return noteDb;
        }
    }
}
