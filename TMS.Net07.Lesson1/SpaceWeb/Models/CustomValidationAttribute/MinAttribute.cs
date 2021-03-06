﻿using System;
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
            return ErrorMessage ?? $"Больше {MinValue}" ;
        }

        public override bool IsValid(object value)
        {
            var number = value as int?;
            if (number == null)
            {
                return false;
            }

            return number > MinValue;
        }
    }
}
