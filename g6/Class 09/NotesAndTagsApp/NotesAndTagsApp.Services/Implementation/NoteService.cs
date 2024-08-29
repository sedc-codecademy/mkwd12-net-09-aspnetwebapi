using Microsoft.EntityFrameworkCore.Migrations;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTAgsApp.Domain.Models;
using NotesAndTagsApp.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesAndTagsApp.Shared.CustomExceptions;

namespace NotesAndTagsApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        //we don't depend on the implementation
        private readonly IRepository<Note> _noteRepository; //DI
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository,
            IRepository<User> userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //validations
            if (addNoteDto == null)
            {
                throw new NoteDataException("Note cannot be null");
            }

            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text cannot be empty string");
            }

            if (addNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text cannot contain more that 100 characters");
            }

            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} was not found");
            }
            //here we have addNoteDto, but our repo needs a note

            //map to domain
            Note newNote = addNoteDto.ToNote(); //NoteMapper.ToNote(addNoteDto)
            newNote.User = userDb;

            //add to db
            _noteRepository.Add(newNote);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            var notesDb = _noteRepository.GetAll(); //here we have a List<Note>

            //we need to return List<NoteDto>

            //for each note it will call the ToNoteDto method from the mapper
            return notesDb.Select(x => x.ToNoteDto()).ToList();
            //return notesDb.Select(x => new NoteDto
            //{
            //    Tag = x.Tag,
            //    Priority = x.Priority,
            //    Text = x.Text,
            //    UserFullName = $"{x.User.Firstname} {x.User.Lastname}"
            //}).ToList();
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            NoteDto noteDto = noteDb.ToNoteDto(); //map from domain to dto
            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto updateNoteDto)
        {
            //validation
            if (updateNoteDto == null)
            {
                throw new NoteDataException("Note cannot be null");
            }

            Note noteDb = _noteRepository.GetById(updateNoteDto.Id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {updateNoteDto.Id} was not found");
            }

            if (string.IsNullOrEmpty(updateNoteDto.Text))
            {
                throw new NoteDataException("Text cannot be empty string");
            }

            if (updateNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text cannot contain more that 100 characters");
            }

            User userDb = _userRepository.GetById(updateNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {updateNoteDto.UserId} was not found");
            }

            //update
            noteDb.Text = updateNoteDto.Text;
            noteDb.Priority = updateNoteDto.Priority;
            noteDb.Tag = updateNoteDto.Tag;
            noteDb.UserId = updateNoteDto.UserId;
            noteDb.User = userDb;

            _noteRepository.Update(noteDb);
        }
    }
}
