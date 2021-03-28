using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SpaceWeb.Models.CustomValidationAttribute
{
    public class MyPhoneAttribute : ValidationAttribute
    {
        const string validationPattern = @"(?<countryCode>\d{1,3})(?<operatorCode>\d{2})(?<phoneNumber>\d{7})$";
        static Regex regex = new Regex(validationPattern);
        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"Неравильный номер";
        }

        public override bool IsValid(object value)
        {
            string phoneString = value as string;
            if (phoneString == null)
                return false;
            return regex.IsMatch(phoneString);
        }
    }
}