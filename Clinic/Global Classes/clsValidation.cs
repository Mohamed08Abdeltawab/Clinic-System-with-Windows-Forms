using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clinic.Global_Classes
{
    public class clsValidation
    {
        //validating email
        public static bool ValidateEmail(string email)
        {
            //initialize pattern 
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            //use new Regex -> new object from Regex class and pass the pattern to it
            //what is regex -> regular expression is a sequence of characters that defines a search pattern, often used for string matching and manipulation. It allows you to specify complex patterns for validating and extracting information from text.
            var regex = new Regex(pattern);

            //return true if the email matches the pattern, otherwise return false
            return regex.IsMatch(email);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }
    }
}
