using Qinshift.MovieRent.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qinshift.MovieRent.DataAccess.Interface
{
    public interface IMovieRepository : IRepository<Movie>
    {
    }
}
