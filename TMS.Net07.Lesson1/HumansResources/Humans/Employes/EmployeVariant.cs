using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    class EmployeVariant : Person
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
        public Department Department { get; private set; }

        //Пустой конструктор
        public EmployeVariant()
        {
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

        /* Можно создавать несколько конструкторов, этот конструктор предназначен для случая,
        когда нам известно в какой департамент пойдет специалист.*/
        public EmployeVariant(Person person, Specification specificationEmploye, Department department)
        {
            Person = person;
            SpecificationEmploye = specificationEmploye;
            Department = department;
        }

        //Можно сделать такой конструктор
        public EmployeVariant(Person person)
        {
            Person = person;
            SpecificationEmploye = Specification.Unknown;
        }

        //Без этого метода вполне можно обойтоись для этого сделать открытым поле Department в setter
        public void SetDepartment(Department department)
        {
            Department = department;
        }
    }
    /* И по поводу коммитов, просто "commit" как-то не очень, коммит должен содержать 
    какую-то полезную информацию. Посмотри - https://www.conventionalcommits.org/en/v1.0.0/*/
}