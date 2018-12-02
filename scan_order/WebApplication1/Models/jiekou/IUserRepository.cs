using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers.jiekou
{
   public interface IUserRepository
{
        IEnumerable<User> Users { get; }
}
}
