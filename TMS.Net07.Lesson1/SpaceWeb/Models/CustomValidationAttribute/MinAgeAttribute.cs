using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models.CustomValidationAttribute
{
    public class MinAgeAttribute : ValidationAttribute
    {
        public int MinValue { get; set; }
        public MinAgeAttribute(int age)
        {
            MinValue = age;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"Больше {MinValue}";
        }

        public override bool IsValid(object value)
        {
            var number = value as DateTime?;
            if (number == null)
            {
                return false;
            }
            // нужно продумать больше логики
            return DateTime.Now.Year - number.Value.Year > MinValue - 1;
        }
    }
}