
namespace HumansResources.Humans.Persons
{
    interface IContact
    {
        string Name { get; }
        string Email { get; }
        PhoneNumber PhoneNumber { get; }
        PostAddress PostAddress { get; }
        
    }
}