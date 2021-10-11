using FootBallLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories.Interfaces
{
    public interface IRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IQueryable<TEntity>> FindAllAsync();
        Task<IQueryable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> TryFindEntityByIdAsync(TKey id,bool tracked);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
