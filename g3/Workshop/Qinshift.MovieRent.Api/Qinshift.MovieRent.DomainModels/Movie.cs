using Qinshift.MovieRent.DomainModels.Enums;

namespace Qinshift.MovieRent.DomainModels
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
    }

}
