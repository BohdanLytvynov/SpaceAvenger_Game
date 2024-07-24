using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataBase.Interfaces
{
    public interface IDatabase<DbType>
    {
        public DbType Db { get; }
    }
}
