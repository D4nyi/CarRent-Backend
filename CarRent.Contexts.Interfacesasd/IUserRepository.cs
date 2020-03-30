﻿using CarRent.Contexts.Models.Core;

namespace CarRent.Contexts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool Validate(string email, string password);
    }
}
