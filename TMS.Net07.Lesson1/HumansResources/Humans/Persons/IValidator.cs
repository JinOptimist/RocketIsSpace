using System.Text.RegularExpressions;

namespace HumansResources.Humans.Persons
{
    /// <summary>
    /// Проверяет на правильность введенную строку. Требуется переопределять шаблон в классе, испуользующий интерфейс
    /// </summary>
    interface IValidator
    {
        //private static Regex regex;
        protected string pattern { get; }
        public bool Validation(string stringToValidation)
        {
            //regex = new Regex(pattern);

            return Regex.IsMatch(stringToValidation, pattern);
        }
    }
}