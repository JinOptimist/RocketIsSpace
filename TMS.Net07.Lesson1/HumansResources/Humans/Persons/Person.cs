
namespace HumansResources.Humans.Persons
{
    public class Person : IContact
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public PhoneNumber PhoneNumber { get; }

        public PostAddress PostAddress { get; }

        Email IContact.Email => throw new System.NotImplementedException();

        public Person()
        {
            Name = null;
            Email = null;
            PhoneNumber = new PhoneNumber("543");
            PostAddress = new PostAddress();
        }
    }
}