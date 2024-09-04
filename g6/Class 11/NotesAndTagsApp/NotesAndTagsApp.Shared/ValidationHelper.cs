using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Shared
{
    public static class ValidationHelper
    {
        public static void ValidateRequiredStringColumnLength(string value, string field, int maxNumOfChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DataException($"{field} is required field!");
            }

            if (value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }

        public static void ValidateStringColumnLength(string value, string field, int maxNumOfChars)
        {
            if(value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }
    }
}
