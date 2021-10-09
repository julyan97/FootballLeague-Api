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
        public IQueryable<T> FindAll()
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<T>();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            //if you doesn't want to be tracked .AsNoTracking();
            return this.repositoryContext.Set<T>().Where(expression);
        }
        public void Create(T entity)
        {
            this.repositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.repositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.repositoryContext.Set<T>().Remove(entity);
        }
    }
}
