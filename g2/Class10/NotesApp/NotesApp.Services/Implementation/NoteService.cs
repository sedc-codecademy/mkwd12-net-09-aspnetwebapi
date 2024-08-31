using NotesApp.DataAccess;
using NotesApp.Domain.Models;
using NotesApp.Dto.NoteDto;
using NotesApp.Mappers;
using NotesApp.Services.Interface;
using NotesApp.Shared.CustomException;

namespace NotesApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository, IRepository<User> userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //validation for user
            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist!");
            }

            //Text
            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (addNoteDto.Text.Length > 250)
            {
                throw new NoteDataException("Text can not contain more than 250 characters!");
            }

            //mapping
            Note newNote = addNoteDto.ToNote();
            newNote.User = userDb;

            //add to db
            _noteRepository.Add(newNote);
        }

        public void DeleteNote(int id)
        {
            //validation
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            var notesDb = _noteRepository.GetAll();
            return notesDb.Select(x => x.ToNoteDto()).ToList();
        }

        public NoteDto GetNoteById(int id)
        {
            //validation
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }

            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            //validation for NoteId
            Note noteDb = _noteRepository.GetById(note.Id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {note.Id} was not found!");
            }
            //validation for User
            User userDb = _userRepository.GetById(note.UserId);
            if (userDb == null)
            {
                throw new NoteNotFoundException($"User with id {note.UserId} was not found!");
            }

            //Text
            if (string.IsNullOrEmpty(note.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (note.Text.Length > 250)
            {
                throw new NoteDataException("Text can not contain more than 250 characters!");
            }

            //Update
            noteDb.Text = note.Text;
            noteDb.Tag = note.Tag;
            noteDb.Priority = note.Priority;
            noteDb.UserId = note.UserId;
            noteDb.User = userDb;

            //update in db
            _noteRepository.Update(noteDb);
        }

    }
}
