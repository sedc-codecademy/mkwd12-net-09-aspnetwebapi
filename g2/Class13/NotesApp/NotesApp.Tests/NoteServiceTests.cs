using NotesApp.DataAccess;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Services.Implementation;
using NotesApp.Services.Interfaces;
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
        public void Setup()
        {
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

    }
}
