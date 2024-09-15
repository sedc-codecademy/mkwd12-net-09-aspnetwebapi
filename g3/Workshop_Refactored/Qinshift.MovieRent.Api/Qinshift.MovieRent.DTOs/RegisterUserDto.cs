using System.ComponentModel.DataAnnotations;

namespace Qinshift.MovieRent.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Please add firstname.")]
        [MinLength(2, ErrorMessage = "Firstname must contain at least 2 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please add lastname.")]
        [MinLength(2, ErrorMessage = "Lastname must contain at least 2 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please add username.")]
        [MinLength(2, ErrorMessage = "Username must contain at least 2 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please add password.")]
        [MinLength(2, ErrorMessage = "Password must contain at least 2 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        public string ConfirmPassword { get; set; }
    }
}
