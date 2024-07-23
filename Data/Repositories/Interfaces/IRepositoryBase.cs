using Data.Entities.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : IIdEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(BsonExpression predicate);

        Task<BsonValue?> AddAsync(TEntity entity);

        Task<bool> RemoveAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<TEntity> GetById(Guid id);
    }
}
