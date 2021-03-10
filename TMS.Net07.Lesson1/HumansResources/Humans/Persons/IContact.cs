
namespace HumansResources.Humans.Persons
{
    public interface IContact
    {
        string Name { get; }

        Email Email { get; }

        PhoneNumber PhoneNumber { get; }

        PostAddress PostAddress { get; }
    }
}