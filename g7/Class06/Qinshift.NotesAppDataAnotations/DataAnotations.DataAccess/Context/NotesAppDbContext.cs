using DataAnotations.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAnotations.DataAccess.Context
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
