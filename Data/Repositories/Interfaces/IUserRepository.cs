using Models.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
       // Extend with Special User Functions
    }
}
