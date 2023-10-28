using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public bool IsValidDOB(object? value)
        {
            if (value == null)
            {
                return false;
            }
            DateTime? date = value as DateTime?;
            if (date.HasValue)
            {
                DateTime currentDate = DateTime.Now;
                if (date <= currentDate && date.Value.Year >= 1900)
                {
                    try
                    {
                        DateTime testDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
                        return true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool checkEmailPattern(string email)
        {
            if (email == null)
            {
                return false;
            }
            if (!Regex.IsMatch(email, @"^.+@[^\.].*\.[a-z]{2,3}$"))
            {
                return false;
            }
            return true;
        }

        public bool checkTelephoneFormat(string phone)
        {
            if (phone.Any(char.IsLetter) || (phone.Length > 13) || (phone.Length < 10))
            {
                return false;
            }
            return true;
        }

        public bool CheckContainDegit(string input)
        {
            return input.Any(char.IsDigit);
        }
    }
}