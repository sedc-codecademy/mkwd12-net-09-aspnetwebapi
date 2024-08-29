using NotesAndTAgsApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTAgsApp.Domain.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text {  get; set; }

        public PriorityEnum Priority { get; set; }

        public TagEnum Tag { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
