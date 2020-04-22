﻿using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;

using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;

namespace CarRent.Contexts.SQLiteContext
{
    public class UserRepositroy : Repository<User>, IUserRepository
    {
        private readonly Lazy<PasswordHasher<User>> _hasher = new Lazy<PasswordHasher<User>>();

        public UserRepositroy(SQLiteDbContext context) : base(context) { }

        public User FindByEmail(string email)
        {
            return _set.FirstOrDefault(f => f.Email == email);
        }

        public User Register(User user, string password)
        {
            user.PasswordHash = _hasher.Value.HashPassword(user, password);

            return _set.Add(user).Entity;
        }

        public bool Validate(string email, string password)
        {
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
