using SEDC.MoviesApp.Domain.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.MoviesApp.Domain
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
