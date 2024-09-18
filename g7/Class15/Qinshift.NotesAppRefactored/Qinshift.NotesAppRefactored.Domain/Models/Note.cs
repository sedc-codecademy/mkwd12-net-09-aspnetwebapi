using Qinshift.NotesAppRefactored.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qinshift.NotesAppRefactored.Domain.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority {  get; set; }
        public Tag Tag { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
