using HumansResources.Humans.Persons;

namespace SpaceWeb.Models.RocketModels
{
    public class RocketProfileViewModel:Person
    {
        public RocketProfileViewModel(RocketRegistrationViewModel model)
        {
            Person = new Person(null, null, null)
            {
                Name = model.Name, Surname = model.LastName, BirthDate = model.DateOfBirth
            };
            UserName = model.UserName;
            Password = model.Password;
        }
        public Person Person { get; set; }

        public new string Email { get; set; }
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}