using Moq;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.DTOs.Notes;
using NotesAndTagsApp.Services.Implementation;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared.CustomExceptions;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Tests
{
    [TestClass]
    public class NoteServiceUnitTests
    {

        private readonly INoteService _noteService;
        private readonly Mock<IRepository<Note>> _noteRepository;
        private readonly Mock<IUserRepository> _userRepository;

        public NoteServiceUnitTests()
        {
            _noteRepository = new Mock<IRepository<Note>>();
            _userRepository = new Mock<IUserRepository>();  
            _noteService = new NoteService(_noteRepository.Object, _userRepository.Object);
        }

        [TestMethod]
        public void GetAllNotes_should_return_NotesDtos()
        {
            //Arrange - test data
            List<Note> notes = new List<Note>()
            {
                new Note
                {
                    Id = 1,
                    Tag = NotesAndTAgsApp.Domain.Enums.TagEnum.SEDC,
                    Text ="Do your homework",
                    Priority = NotesAndTAgsApp.Domain.Enums.PriorityEnum.High,
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        Firstname = "Test1",
                        Lastname = "User1"
                    }
                },

                new Note
                {
                    Id = 2,
                    Tag = NotesAndTAgsApp.Domain.Enums.TagEnum.Exercise,
                    Text ="Go to the gym",
                    Priority = NotesAndTAgsApp.Domain.Enums.PriorityEnum.High,
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        Firstname = "Test2",
                        Lastname = "User2"
                    }
                },
            };

            _noteRepository.Setup(x => x.GetAll()).Returns(notes); //instead of calling GetAll from the repo, return the notes list from the arrange part


            //Act
            List<NoteDto> noteDtos = _noteService.GetAllNotes();

            //Assert
            Assert.AreEqual(2, noteDtos.Count);
            Assert.AreEqual("Do your homework", noteDtos.First().Text);
            Assert.AreEqual("Test1 User1", noteDtos.First().UserFullName);
        }

        [TestMethod]
        public void GetAllNotes_ShouldReturnEmptyList_on_EmptyListFromDb()
        {
            //Arrange
            //simulate that no data was returned from db
            _noteRepository.Setup(x => x.GetAll()).Returns(new List<Note>());


            //Act
            List<NoteDto> noteDtos = _noteService.GetAllNotes();

            //Assert
            Assert.AreEqual(0, noteDtos.Count);
        }

        [TestMethod]
        public void GetById_ShouldTHrow_On_NoteNotFound()
        {
            //Arrange
            int id = 2;
            _noteRepository.Setup(x => x.GetById(id)).Returns(null as Note); //we simulate that the note with id 2 was not found (the db returned null)
           
            //Act and assert
            var exception = Assert.ThrowsException<NoteNotFoundException>(() => _noteService.GetById(id));  //check if this exception was thrown

            //Assert

            Assert.AreEqual($"Note with id {id} was not found", exception.Message); //check if this was the message of the exception


        }
    }
}
