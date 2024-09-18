using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qinshift.MovieRent.Shared.Exceptions
{
    public class MovieDataException : Exception
    {
        public MovieDataException(string message) : base(message)
        {
            
        }
    }
}
