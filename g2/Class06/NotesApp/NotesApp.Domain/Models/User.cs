using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Domain.Models
{
    // Configuring Entities using Data Annotations
    //[Table("Userss")]
    public class User : BaseEntity
    {
        //[Required]
        //[MaxLength(100)]
        public string FirstName { get;set; }
        //[Required]
        //[MaxLength(100)]
        public string LastName { get;set; }
        public string Username { get;set; }
        [MaxLength(1000)]
        public string Password { get;set; }
        //[NotMapped]
        public int Age { get; set; }

        // Navigation property
        public virtual List<Note> Notes { get; set; } = new List<Note>();
    }
}
