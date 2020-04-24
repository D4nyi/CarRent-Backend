using CarRent.Contexts.Models.Core;

namespace CarRent.Contexts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByEmail(string email);
        User Register(User user, string password);
        bool Validate(string email, string password);
        bool EmailExists(string email);
        bool UserNameExists(string userName);
    }
}
