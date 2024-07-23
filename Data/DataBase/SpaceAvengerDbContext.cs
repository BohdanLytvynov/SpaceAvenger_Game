using Data.DataBase.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataBase
{
    public class SpaceAvengerDbContext : IDatabase<LiteDatabase>
    {
        #region Fields

        LiteDatabase m_db;

        #endregion

        #region Properties

        public LiteDatabase Db { get => m_db; }

        #endregion

        #region Ctor

        public SpaceAvengerDbContext(string path)
        {
            m_db = new LiteDatabase(path);
        }

        #endregion
    }
}
