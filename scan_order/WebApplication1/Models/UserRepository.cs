using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers.jiekou
{
    public class UserRepository :IUserRepository
    {
        private UserDbContext userDbContext = new UserDbContext();

        public IEnumerable<User> Users => userDbContext.Users;
    }
}