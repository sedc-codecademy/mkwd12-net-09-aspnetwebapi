using NotesAndTagsApp.DTOs;
using NotesAndTAgsApp.Domain.Models;

namespace NotesAndTagsApp.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Tag = note.Tag,
                Priority = note.Priority,
                Text = note.Text,
                //UserFullName = $"{note.User.Firstname} {note.User.Lastname}" //if we don't include the user when we return from repo, user will be null
            };
        }

        public static Note ToNote(this AddNoteDto addNote)
        {
            return new Note()
            {
                Text = addNote.Text,
                Priority = addNote.Priority,
                Tag = addNote.Tag,
                UserId = addNote.UserId //FK
            };
        }
    }
}
