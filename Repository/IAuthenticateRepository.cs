using System;
using System.Collections.Generic;
using Commander.DTOModels;
using Commander.Models;

namespace Commander.Repository
{
    public interface IAuthenticateRepository
    {
        ResponseModelDTO Authenticate(UserModel user);
        UserModel GetUserById(Guid id);
        IEnumerable<UserModel> ListUsers();
        bool SaveChanges();
    }
}