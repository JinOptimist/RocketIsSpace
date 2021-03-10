
namespace HumansResources.Humans.Persons
{
    public class PostAddress
    {
        /// <summary>
        /// Получатель
        /// </summary>
        public string Addressee { get; set; }
        /// <summary>
        /// Название компании (если указывается рабочий адрес)
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public string HouseNumber { get; set; }
        /// <summary>
        /// Корпус
        /// </summary>
        public string Housing { get; set; }
        /// <summary>
        /// Район
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostIndex { get; set; }
        /// <summary>
        /// Лучше заполнять через параметры -> PostAddress {Addressee = "Kirill", Country = "Belarus"}
        /// </summary>
        public PostAddress()
        {
            Addressee = "пусто";
            CompanyName = "пусто";
            Street = "пусто";
            HouseNumber = "пусто";
            Housing = "пусто";
            District = "пусто";
            City = "пусто";
            Country = "пусто";
            PostIndex = "пусто";
        }
        public override string ToString()
        {
            return 
                $"{Addressee}" +
                $"{CompanyName}" +
                $"{Street}, {HouseNumber}, {Housing}" +
                $"{District}" +
                $"{City}" +
                $"{Country}" +
                $"{PostIndex}";
        }
    }
}