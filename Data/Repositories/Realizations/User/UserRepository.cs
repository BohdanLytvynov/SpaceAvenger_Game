using Data.DataBase.Interfaces;
using Data.Repositories.Interfaces;
using Data.Repositories.Realizations.Base;
using LiteDB;
using Models.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Realizations.UserRep
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabase<LiteDatabase> db) : base(db)
        {
            
        }
    }
}
