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
        /// <summary>
        /// return all Entities of the requred type
        /// </summary>
        /// <returns>returns all Entities of the requred type</returns>
        Task<IQueryable<TEntity>> FindAllAsync();

        /// <summary>
        /// Lets you find and entity by a condition expressed as a lambda expresion(delegate function)
        /// </summary>
        /// <param name="expression"> Func<TEntity, bool> function to express the condition</param>
        /// <returns> returns the entities that have met the requirements </returns>
        Task<IQueryable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Lets you find an entity by its given id, if it doesnt exist throws and ArgumentException
        /// </summary>
        /// <param name="id">Id of the required entity</param>
        /// <param name="tracked">indicates if we want the returned entity to be tracked by ef core</param>
        /// <returns>returns the requested entity</returns>
        Task<TEntity> TryFindEntityByIdAsync(TKey id,bool tracked);

        /// <summary>
        /// Creates the requested entity
        /// </summary>
        /// <param name="entity">entity to be created</param>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Updates a requested entity
        /// </summary>
        /// <param name="entity">entity we want to Update</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes a requested entity
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);
    }
}
