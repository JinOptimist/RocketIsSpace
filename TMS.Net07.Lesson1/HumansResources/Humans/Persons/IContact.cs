
namespace HumansResources.Humans.Persons
{
    public interface IContact
    {
        string Name { get; }
        string Email { get; }
        PhoneNumber PhoneNumber { get; }
        PostAddress PostAddress { get; }
        
    }
}