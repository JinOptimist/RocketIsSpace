using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public class EmployeVariant : Person
    {

        /*В случае со Specification, наверное целый класс не нужен, достаточно одного Enum,
        но если заморочиться можно создать и добавить туда, например, образование - высшее, 
        специально-техническое и др.*/
        public enum Specification
        {
            Leader,
            Spaceman,
            Scientist,
            Еngineer,
            Technicist,
            Other,
            Unknown
        };

        /*Получив объект Person, ты автоматически получишь доступ ко всем его публичным (доступным) полям,
        а гадать, какие поля у него будут в конечном итоге сейчас не нужно. Тем более они тебе сейчас 
        не очень то и нужны.*/
        public Person Person { get; set; }

        // Просто сделать Specification Specification не получится, поэтому Specification SpecificationEmploye
        public Specification SpecificationEmploye { get; set; }
        

        //Пустой конструктор
        public EmployeVariant()
        {
        }

        //Можно сделать такой конструктор
        public EmployeVariant(Person person)
        {
            Person = person;
            SpecificationEmploye = Specification.Unknown;
        }

        /*Нам совсем необязательно передавать все параметры через конструктор.
        Как правило, через конструктор передают минимум, необходимый для работоспособности класса
        или его целесообразности. Для того, чтобы можно было заполнить остальные поля у нас есть setters.
        В нашем случае, у нас вполне может быть просто человек со специальностью, а далее его назначат 
        в какой-то департамент.*/
        public EmployeVariant(Person person, Specification specificationEmploye)
        {
            Person = person;
            SpecificationEmploye = specificationEmploye;
        }        
       
    }
    /* И по поводу коммитов, просто "commit" как-то не очень, коммит должен содержать 
    какую-то полезную информацию. Посмотри - https://www.conventionalcommits.org/en/v1.0.0/*/
}