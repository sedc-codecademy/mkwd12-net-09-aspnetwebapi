using Qinshift.NotesAppRefactored.Dtos.NoteDtos;
using Qinshift.NotesAppRefactored.Services.Implementations;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.NoteExceptions;
using Qinshift.NotesAppRefactored.Tests.FakeRepositories;

namespace Qinshift.NotesAppRefactored.Tests
{
    [TestClass] // to tell that this class will contain tests
    public class NoteTests
    {
        [TestMethod] // make the method testMethofd
        public void AddNote_InvalidUserId_Exception()
        {
            //arrange (setting up the environment)
            INoteService noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = Domain.Enums.Priority.Low,
                Tag = Domain.Enums.Tag.Work,
                Text = "Do your work!",
                UserId = 2 // in our staticDb in our fakeRepo, such a user doesnt exist
            };

            //act & assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));
        }

        [TestMethod]
        public void AddNote_EmptyText_Exception()
        {
            //arrange
            INoteService noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = Domain.Enums.Priority.Low,
                Tag = Domain.Enums.Tag.Work,
                Text = "", // Our Note class cannot be instanciated without text field having value
                UserId = 1
            };

            //act & assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));
        }

        [TestMethod]
        public void AddNote_LargerText_Exception()
        {
            INoteService noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = Domain.Enums.Priority.Low,
                Tag = Domain.Enums.Tag.Work,
                //constaingt with text property having maximum 100 characters
                Text = "asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd",
                UserId = 1
            };

            //act & assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));
        }

        [TestMethod]
        public void GetAllNotes_Count()
        {
            //arrange
            var noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());
            var expectedCount = 2;

            //act
            var result = noteService.GetAllNotes();

            //assert
            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        public void GetNoteById_InvalidId_Exception()
        {
            //arrange
            var noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());

            //act & assert
            //we dont have a note with id = 3 in our fake repo
            Assert.ThrowsException<NoteNotFoundException>(() => noteService.GetById(3));
        }

        [TestMethod]
        public void GetNoteById_ValidUser_NoteDto()
        {
            //arrange
            var noteService = new NoteService(new FakeNoteRepository(), new FakeUserRepository());
            var expectedNoteText = "Do something";

            //act
            var result = noteService.GetById(1);

            //assert
            Assert.AreEqual(result.Text, expectedNoteText);
        }
    }
}
