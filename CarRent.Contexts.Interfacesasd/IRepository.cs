using System;
using System.Collections.Generic;

/// <summary>
/// Core interfaces for DB providers
/// </summary>
namespace CarRent.Contexts.Interfaces
{
    /// <summary>
    /// Base interface for repositories
    /// </summary>
    /// <typeparam name="TEntity">Represents a model class from DB Context</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Adds the an instance to the local context and keeps tracking its state,
        /// NOT COMMITTED TO THE DB!
        /// </summary>
        /// <param name="entity">The instance to be added</param>
        /// <returns>The added instance</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds the provided collection of entities to the local context
        /// </summary>
        /// <param name="entities">Entities to be added</param>
        /// <returns>Success of the addition</returns>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets one entry from the DB by the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Search param</returns>
        TEntity Find(string id);

        /// <summary>
        /// Gets all entries from the DB
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TEntry}"/> collection of <see cref="{TEntity}"/></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Deletes the provided entry
        /// </summary>
        /// <param name="id">Search param</param>
        TEntity Delete(string id);

        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves every changes to the DB
        /// </summary>
        /// <returns>number of changes</returns>
        int Save();
    }
}
