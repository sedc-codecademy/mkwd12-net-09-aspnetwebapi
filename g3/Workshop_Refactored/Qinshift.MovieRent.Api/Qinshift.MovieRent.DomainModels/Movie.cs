using Qinshift.MovieRent.DomainModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qinshift.MovieRent.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Plot { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public Genre Genre { get; set; }
    }

}
