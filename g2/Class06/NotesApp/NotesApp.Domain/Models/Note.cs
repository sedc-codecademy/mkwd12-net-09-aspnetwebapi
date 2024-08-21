using NotesApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Domain.Models
{
    // Configuring Entities using Data Annotations
    //[Table("Notess")]
    public class Note : BaseEntity
    {
        //[Required]
        //[StringLength(500)]
        public string Text { get; set; }
        //[Required]
        public Priority Priority { get; set; }
        //[Required]
        public Tag Tag { get; set; }

        // Navigation properties
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
