using SpaceWeb.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.CustomValidationAttribute
{
    public class MinAttribute : ValidationAttribute
    {
        public int MinValue { get; set; }

        public MinAttribute(int minValue)
        {
            MinValue = minValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"{name} {Resource.Error_Message_Min_Attr} {MinValue}" ;
        }

        public override bool IsValid(object value)
        {
            var result = Int32.TryParse(value.ToString(), out int number);
            return result == false ? result : number > MinValue;
        }
    }
}
