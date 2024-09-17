using Qinshift.NotesAppRefactored.Data;
using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Dtos.NoteDtos;
using Qinshift.NotesAppRefactored.Mappers.NoteMappings;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.NoteExceptions;

namespace Qinshift.NotesAppRefactored.Services.Implementations
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
            // 1. Validation
            User UserDb = _userRepository.GetById(addNoteDto.UserId);
            if (UserDb == null)
                throw new NoteDataException($"User wit id {addNoteDto.UserId} does not exist");

            if (string.IsNullOrEmpty(addNoteDto.Text))
                throw new NoteDataException("Text field is required");

            if (addNoteDto.Text.Length > 100)
                throw new NoteDataException("Text field can not contain more that 100 characters");

            //2. map addNoteDto to our domain model Note
            Note newNote = addNoteDto.ToNote();
            newNote.User = UserDb;

            //3. add the note to the database
            _noteRepository.Add(newNote);
        }

        public void DeleteNote(int id)
        {
            //first we are getting the note from our DB
            Note noteDb = _noteRepository.GetById(id);

            //validation
            if (noteDb == null)
                throw new NoteNotFoundException($"Note with id {id} was not found");

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            var NotesDb = _noteRepository.GetAll();
            return NotesDb.Select(x => x.ToNoteDto()).ToList();
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);

            if (noteDb == null)
                throw new NoteNotFoundException($"Note with id {id} was not found");

            NoteDto noteDto = noteDb.ToNoteDto();

            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            //getting the existing note
            Note noteDb = _noteRepository.GetById(note.Id);
            if (noteDb == null)
                throw new NoteNotFoundException($"Note with id {note.Id} was not found!");

            //getting the existing user
            User userDb = _userRepository.GetById(note.UserId);
            if (userDb == null)
                throw new NoteDataException($"User with id {note.UserId} doesnt exists");

            //all sorts of validations here

            noteDb.Text = note.Text;
            noteDb.Priority = note.Priority;
            noteDb.Tag = note.Tag;
            noteDb.User = userDb;
            noteDb.UserId = note.UserId;

            _noteRepository.Update(noteDb);
        }
    }
}
