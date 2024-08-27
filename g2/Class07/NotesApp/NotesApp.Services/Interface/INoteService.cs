using NotesApp.Dto.NoteDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Interface
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
