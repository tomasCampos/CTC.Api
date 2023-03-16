namespace CTC.Application.Shared.Request.Validator
{
    internal static class StringValidationExtensions
    {
        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
