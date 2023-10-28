using System.ComponentModel.DataAnnotations;

namespace Validation
{
    public class Utils
    {
        public bool IsInt(string input)
        {
            return int.TryParse(input, out _);
        }
        public bool IsDecimal(string input)
        {
            return decimal.TryParse(input, out _);
        }
        public bool IsDouble(string input)
        {
            return double.TryParse(input, out _);
        }
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