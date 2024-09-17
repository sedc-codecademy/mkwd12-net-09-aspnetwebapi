using NotesApp.DataAccess;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;
using NotesApp.Dto.NoteDto;
using NotesApp.Services.Implementation;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.CustomException;
using NotesApp.Tests.FakeRepositories;

namespace NotesApp.Tests
{
    [TestClass]
    public class NoteServiceTests
    {
        private INoteService _noteService;
        private IRepository<Note> _fakeNoteRepository;
        private IUserRepository _fakeUserRepository;

        [TestInitialize]
        // Used to prepare the test environment before each individual test method runs
        // Mock Repositories, other dependencies, or any other setup
        public void Setup()
        {
            // ===> Mocking is a technique used in unit testing to create fake objects
            _fakeNoteRepository = new FakeNoteRepository();
            _fakeUserRepository = new FakeUserRepository();
            _noteService = new NoteService(_fakeNoteRepository, _fakeUserRepository);
        }

        [TestMethod]
        public void GetAllNotes_ValidUserId_ShouldReturnAllNotesForUser()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = _noteService.GetAllNotes(userId);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllNotes_InvalidUserId_ShouldReturnEmptyList()
        {
            // Arrange
            int userId = 10;

            // Act
            var result = _noteService.GetAllNotes(userId);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetNoteById_ValidId_ShouldReturnNote()
        {
            // Arrange
            int noteId = 1;

            // Act
            var result = _noteService.GetNoteById(noteId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(noteId, result.Id);
            Assert.AreEqual("Do something", result.Text);
        }

        [TestMethod]
        public void GetNoteById_InvalidId_ShouldThrowException()
        {
            // Arrange
            int noteId = 100;

            // Act & Assert
            Assert.ThrowsException<NoteNotFoundException>(() => _noteService.GetNoteById(noteId));
        }

        [TestMethod]
        public void AddNote_InvalidUser_ShouldThrowException()
        {
            // Arrange
            var addNoteDto = new AddNoteDto
            {
                UserId = 2,
                Text = "Invalid User",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act & Assert
            Assert.ThrowsException<NoteDataException>(() => _noteService.AddNote(addNoteDto));
        }

        [TestMethod]
        public void AddNote_EmptyText_ShouldThrowException()
        {
            // Arrange
            var addNoteDto = new AddNoteDto
            {
                UserId = 1,
                Text = "",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act & Assert
            Assert.ThrowsException<NoteDataException>(() => _noteService.AddNote(addNoteDto));
        }

        [TestMethod]
        public void AddNote_TextTooLong_ShouldThrowException()
        {
            // Arrange
            var addNoteDto = new AddNoteDto()
            {
                UserId = 1,
                Text = "fsdfsfdsfdsfdsdfsdfssdfsdfsdfsdfsdfdfsdfsdfssdfdsfsdfsdfdsfsfsdfsdfsdfsdfsdfsdfsdfsdfsdfssdfsdfsdfsdfsdfsdfsdfsdfsfdsfsdfsdfsfdsfdsfdsdfsdfssdfsdfsdfsdfsdfdfsdfsdfssdfdsfsdfsdfdsfsfsdfsdfsdfsdfsdfsdfsdfsdfsdfssdfsdfsdfsdfsdfsdfsdfsdfsfdsfsdfsdfsfdsfdsfdsdfsdfssdfsdfsdfsdfsdfdfsdfsdfssdfdsfsdfsdfdsfsfsdfsdfsdfsdfsdfsdfsdfsdfsdfssdfsdfsdfsdfsdfsdfsdfsdfsfdsfsd",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act & Assert
            Assert.ThrowsException<NoteDataException>(() => _noteService.AddNote(addNoteDto));
        }

        [TestMethod]
        public void AddNote_ValidData_ShouldAddNote()
        {
            // Arrange
            var addNoteDto = new AddNoteDto()
            {
                UserId = 1,
                Text = "New Note",
                Tag = Tag.Work,
                Priority = Priority.Low
            };
            int initNoteCount = _fakeNoteRepository.GetAll().Count;

            // Act
            _noteService.AddNote(addNoteDto);
            var allNotes = _fakeNoteRepository.GetAll();

            // Assert
            Assert.AreEqual(initNoteCount + 1, allNotes.Count);
            Assert.IsTrue(allNotes.Any(x => x.Text == addNoteDto.Text));
        }

        [TestMethod]
        public void UpdateNote_ValidData_ShouldUpdateNote()
        {
            // Arrange
            var updateNoteDto = new UpdateNoteDto()
            {
                Id = 1,
                UserId = 1,
                Text = "Updated Note",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act
            _noteService.UpdateNote(updateNoteDto);
            var updatedNote = _fakeNoteRepository.GetById(1);

            // Assert
            Assert.AreEqual("Updated Note", updatedNote.Text);
            Assert.AreEqual(Tag.Work, updatedNote.Tag);
            Assert.AreEqual(Priority.Low, updatedNote.Priority);
        }

        [TestMethod]
        public void UpdateNote_InvalidNoteId_ShouldThrowException()
        {
            // Arrange
            var updateNoteDto = new UpdateNoteDto()
            {
                Id = 99,
                UserId = 1,
                Text = "Invalid Update",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act & Assert
            Assert.ThrowsException<NoteNotFoundException>(() => _noteService.UpdateNote(updateNoteDto));
        }

        [TestMethod]
        [ExpectedException(typeof(NoteNotFoundException))]
        public void UpdateNote_InvalidUserId_ShouldThrowException()
        {
            // Arrange
            var updateNoteDto = new UpdateNoteDto()
            {
                Id = 1,
                UserId = 99, // Invalid user ID
                Text = "Invalid Update",
                Tag = Tag.Work,
                Priority = Priority.Low
            };

            // Act
            _noteService.UpdateNote(updateNoteDto);
        }

        [TestMethod]
        public void DeleteNote_ValidId_ShouldDeleteNote()
        {
            // Act
            _noteService.DeleteNote(1);
            var allNotes = _fakeNoteRepository.GetAll();

            // Assert
            Assert.AreEqual(1, allNotes.Count);
            Assert.IsFalse(allNotes.Any(x => x.Id == 1));
        }

        [TestMethod]
        [ExpectedException(typeof(NoteNotFoundException))]
        public void DeleteNote_InvalidId_ShouldThrowException()
        {
            // Act
            _noteService.DeleteNote(99); // Invalid ID
        }

    }
}
