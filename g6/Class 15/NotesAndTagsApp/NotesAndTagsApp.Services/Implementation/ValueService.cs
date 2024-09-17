using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Implementation
{
    public class ValueService
    {

        public int? SumPositiveNumbers(int num1, int num2)
        {
            if(num1< 0 || num2< 0)
            {
                return null;
            }

            return num1 + num2;
        }

        public bool IsFirstNumberLarger(int num1, int num2)
        {
            return num1 > num2;
        }
    }

}
