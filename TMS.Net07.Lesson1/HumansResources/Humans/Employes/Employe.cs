
using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes

{
    public class Employe : Person
    {
        public int BankAccaunt { get; set; }
        public int ID { get; set; }

        public Employe() { }

        public Employe(string name, int age, string surname, Gender gender, int bankAccaunt, int id) : base(name, age, surname, gender)
        {
            BankAccaunt = bankAccaunt;
            ID = id;

        }
    }
}