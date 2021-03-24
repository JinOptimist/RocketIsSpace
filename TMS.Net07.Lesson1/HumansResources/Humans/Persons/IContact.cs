
namespace HumansResources.Humans.Persons
{
    public interface IContact
    {
        Email Email { get; }

        PhoneNumber PhoneNumber { get; }

        PostAddress PostAddress { get; }
    }
}