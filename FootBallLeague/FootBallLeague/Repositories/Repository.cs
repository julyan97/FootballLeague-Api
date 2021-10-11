using FootBallLeague.Data;
using FootBallLeague.Models;
using FootBallLeague.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly FootballLeageDbContext repositoryContext;

        public Repository(FootballLeageDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public async Task<IQueryable<TEntity>> FindAllAsync()
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<TEntity>();
        }
        public async Task<IQueryable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<TEntity>().Where(expression);
        }
        public async Task<TEntity> TryFindEntityByIdAsync(TKey id, bool tracked = false)
        {
            TEntity entity = null;
            if (tracked)
            {
                entity = (await FindByConditionAsync(x => x.Id.Equals(id)))
                .AsNoTracking()
                .FirstOrDefault();
            }
            else
            {
                entity = (await FindByConditionAsync(x => x.Id.Equals(id)))
                .FirstOrDefault();
            }

            if (entity == null)
                throw new ArgumentException($"No {nameof(TEntity)} with Id {id} exists");
            return entity;
        }
        public async Task CreateAsync(TEntity entity)
        {
            await this.repositoryContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            this.repositoryContext.Set<TEntity>().Update(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            this.repositoryContext.Set<TEntity>().Remove(entity);
        }
    }
}
