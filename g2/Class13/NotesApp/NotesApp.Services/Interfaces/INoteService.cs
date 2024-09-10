using NotesApp.Dto.NoteDto;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes(int userId);
        NoteDto GetNoteById(int id);
        void AddNote(AddNoteDto addNoteDto);
        void DeleteNote(int id);
        void UpdateNote(UpdateNoteDto note);
    }
}
