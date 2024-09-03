using Qinshift.MovieApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qinshift.MovieApp.Domain.Domain
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
