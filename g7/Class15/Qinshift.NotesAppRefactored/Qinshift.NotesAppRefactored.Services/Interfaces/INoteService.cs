using Qinshift.NotesAppRefactored.Dtos.NoteDtos;

namespace Qinshift.NotesAppRefactored.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();
        NoteDto GetById(int id);
        void AddNote(AddNoteDto note);
        void UpdateNote(UpdateNoteDto note);
        void DeleteNote(int id);
    }
}
