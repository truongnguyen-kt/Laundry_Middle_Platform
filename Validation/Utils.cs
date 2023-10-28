namespace Validation
{
    public static class Utils
    {   
        public static bool IsInt(string input)
        {
            return int.TryParse(input, out _);
        }
        public static bool IsDecimal(string input)
        {
            return decimal.TryParse(input, out _);
        }
        public static bool IsDouble(string input)
        {
            return double.TryParse(input, out _);
        }
        public static bool IsNotEmptyString(string input)
        {
            return !String.IsNullOrEmpty(input);
        }
    }
}