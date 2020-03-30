using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;

namespace CarRent.Contexts.SQLiteContext
{
    public class UserRepositroy : Repository<User>, IUserRepository
    {
        public UserRepositroy(SQLiteDbContext context) : base(context)
        {
        }

        public bool Validate(string email, string password)
        {
            return false;
        }
    }
}
