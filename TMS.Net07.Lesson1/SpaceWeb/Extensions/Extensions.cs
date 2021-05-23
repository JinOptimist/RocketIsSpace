using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SpaceWeb.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Получает строковое представление для поля перечисления
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Возвращает поле Name атрибуа Display, или пустую строку в случае если атрибута нету</returns>
        public static string GetDisplayableName(this Enum value)
        {
            return value.GetType().
                GetMember(value.ToString()).
                SingleOrDefault().
                GetCustomAttribute<DisplayAttribute>()?.
                GetName();
        }
    }
}