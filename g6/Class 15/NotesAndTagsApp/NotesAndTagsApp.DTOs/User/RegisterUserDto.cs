using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DTOs.User
{
    public class RegisterUserDto
    {
        public string Firstname { get; set; }
        public string Lastname {get; set; }
        public string Username {get; set; }
        public string Password {get; set; }
        public string ConfirmPassword {get; set; }
    }
}
