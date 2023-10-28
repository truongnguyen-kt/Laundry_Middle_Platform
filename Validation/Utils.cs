using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Validation
{
    public class Utils
    {
        public bool IsNotEmptyString(string input)
        {
            return !String.IsNullOrEmpty(input);
        }
        public bool IsEmail(string input)
        {
            return input != null && new EmailAddressAttribute().IsValid(input);
        }

    }
}