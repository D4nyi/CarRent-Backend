using CarRent.Contexts.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRent.Contexts.SQLiteContext
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly SQLiteDbContext _context;
        protected readonly DbSet<TModel> _set;

        public Repository(SQLiteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TModel>();
        }

        public TModel Add(TModel entity)
        {
            return _set.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TModel> models)
        {
            _set.AddRange(models);
        }

        public TModel Delete(string id)
        {
            TModel entity = Find(id);
            _set.Remove(entity);

            return entity;
        }

        public void DeleteRange(IEnumerable<TModel> models)
        {
            _set.RemoveRange(models);
        }

        public TModel Find(string id)
        {
            return _set.Find(id);
        }

        public IEnumerable<TModel> GetAll()
        {
            return _set.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
