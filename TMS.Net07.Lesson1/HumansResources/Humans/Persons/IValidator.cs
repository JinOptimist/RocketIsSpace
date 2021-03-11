
namespace HumansResources.Humans.Persons
{
    /// <summary>
    /// Проверяет на правильность введенную строку. Требуется переопределять шаблон в классе, испуользующий интерфейс
    /// </summary>
    public interface IValidator
    {
        string Pattern { get; }

        public bool Validation();
    }
}