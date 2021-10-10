using FootBallLeague.Data;
using FootBallLeague.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly FootballLeageDbContext repositoryContext;

        public Repository(FootballLeageDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public async Task<IQueryable<T>> FindAllAsync()
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<T>();
        }
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<T>().Where(expression);
        }
        public async Task CreateAsync(T entity)
        {
            await this.repositoryContext.Set<T>().AddAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            this.repositoryContext.Set<T>().Update(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            this.repositoryContext.Set<T>().Remove(entity);
        }
    }
}
