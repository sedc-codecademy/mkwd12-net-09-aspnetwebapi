using Microsoft.EntityFrameworkCore.Migrations;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        IRepository<Note> _repo;
        public List<NoteDto> GetAllNotes()
        {
            throw new NotImplementedException();
        }
    }
}
