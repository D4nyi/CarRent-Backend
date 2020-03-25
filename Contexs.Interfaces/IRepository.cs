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
        void Delete(string id);

        /// <summary>
        /// Saves every changes to the DB
        /// </summary>
        void Save();
    }
}
