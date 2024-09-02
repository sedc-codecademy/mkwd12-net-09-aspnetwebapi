using NotesAndTagsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Interfaces
{
    public interface INoteService
    {
        //our controller expects list of noteDtos, that's why the service must return that
        List<NoteDto> GetAllNotes();

        NoteDto GetById(int id);
        void AddNote(AddNoteDto addNoteDto);

        void UpdateNote(UpdateNoteDto updateNoteDto);

        void DeleteNote(int id);   
    }
}
