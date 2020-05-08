using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarRent.Contexts.SQLiteContext
{
    public class UserRepositroy : Repository<User>, IUserRepository
    {
        private readonly Lazy<PasswordHasher<User>> _hasher = new Lazy<PasswordHasher<User>>(() => new PasswordHasher<User>());

        public UserRepositroy(SQLiteDbContext context) : base(context) { }

        public bool EmailExists(string email)
        {
            return _set.Any(x => x.Email == email);
        }

        public User FindByEmail(string email, bool loadRole = false)
        {
            IQueryable<User> query = _set.AsQueryable();

            if (loadRole)
            {
                query = query.Include(i => i.Role);
            }

            return query.FirstOrDefault(f => f.Email == email);
        }

        public string GetUserId(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Either of the arguments are invalid");
            }

            User user = _set.FirstOrDefault(f => f.Email == email);

            if (user is null)
            {
                throw new EntryNotFoundException("User not found!");
            }

            return user.Id;
        }

        public User Register(User user, string password)
        {
            user.PasswordHash = _hasher.Value.HashPassword(user, password);

            return _set.Add(user).Entity;
        }

        public bool UserNameExists(string userName)
        {
            return _set.Any(x => x.UserName == userName);
        }

        public bool Validate(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            User user = _set.FirstOrDefault(f => f.Email == email);

            if (user is null)
            {
                return false;
            }

            PasswordVerificationResult result = _hasher.Value.VerifyHashedPassword(user, user.PasswordHash, password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
