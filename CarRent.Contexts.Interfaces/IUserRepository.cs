using CarRent.Contexts.Models.Core;

namespace CarRent.Contexts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByEmail(string email, bool loadRole = false);

        User Register(User user, string password);
        string GetUserId(string email, string password);
        bool Validate(string email, string password);
        bool EmailExists(string email);
        bool UserNameExists(string userName);
    }
}
