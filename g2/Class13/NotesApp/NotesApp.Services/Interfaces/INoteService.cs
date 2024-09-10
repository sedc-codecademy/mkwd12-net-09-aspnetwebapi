using NotesApp.Dto.NoteDto;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();
        NoteDto GetNoteById(int id);
        void AddNote(AddNoteDto addNoteDto);
        void DeleteNote(int id);
        void UpdateNote(UpdateNoteDto note);
    }
}
