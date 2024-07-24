using Data.DataBase.Interfaces;
using Data.Entities.Interfaces;
using Data.Repositories.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Realizations.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : IIdEntity
    {
        IDatabase<LiteDatabase> m_db;

        public RepositoryBase(IDatabase<LiteDatabase> db)
        {
            m_db = db;
        }

        public async Task<BsonValue?> AddAsync(TEntity entity)
        {
            BsonValue? bsonValue = null;

            await Task.Run(() =>
            {
                var col = m_db.Db.GetCollection<TEntity>();

                if (col == null)
                {
                    throw new NullReferenceException("Fail to find the Collection!");
                }

                if(col.FindOne(x => x.Id.Equals(entity.Id)) is null)
                    bsonValue = col.Insert(entity);

            });
            return bsonValue;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(m_db.Db.GetCollection<TEntity>().FindAll());
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(BsonExpression predicate)
        {
            return await Task.FromResult(m_db.Db.GetCollection<TEntity>().Find(predicate));
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Task.FromResult(m_db.Db.GetCollection<TEntity>().FindById(id));
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            return await Task.FromResult(m_db.Db.GetCollection<TEntity>().Delete(entity.Id));            
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(m_db.Db.GetCollection<TEntity>().Update(entity));
        }                
    }
}
