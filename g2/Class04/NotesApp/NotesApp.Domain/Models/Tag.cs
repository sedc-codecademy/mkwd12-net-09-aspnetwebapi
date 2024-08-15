using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Domain.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
