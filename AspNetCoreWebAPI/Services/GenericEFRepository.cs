using AspNetCoreWebAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace AspNetCoreWebAPI.Services
{
    public class GenericEFRepository : IGenericEFRepository
    {
        private SqlDbContext _db;
        public GenericEFRepository(SqlDbContext db)
        {
            _db = db;
        }

        public IEnumerable<TEntity> Get<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>();
        }

        public TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class
        {
            var entity = _db.Set<TEntity>().Find(new object[] { id });

            if (entity != null && includeRelatedEntities)
            {
                // Get the names of all DbSets in the DbContext
                var dbsets = typeof(SqlDbContext)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(z => z.PropertyType.Name.Contains("DbSet"))
                    .Select(z => z.Name);

                // Get the names of all the properties (tables) in the generic
                // type T that is represented by a DbSet
                var tables = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(z => dbsets.Contains(z.Name))
                    .Select(z => z.Name);

                // Eager load all the tables referenced by the generic type T
                if (tables.Count() > 0)
                    foreach (var table in tables)
                        _db.Entry(entity).Collection(table).Load();
            }

            return entity;
        }

        public void Add<TEntity>(TEntity item) where TEntity : class
        {
            _db.Add<TEntity>(item);
        }

        public void Delete<TEntity>(TEntity item) where TEntity : class
        {
            _db.Set<TEntity>().Remove(item);
        }

        public bool Exists<TEntity>(int id) where TEntity : class
        {
            return _db.Set<TEntity>().Find(new object[] { id }) != null;
        }
        
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

    }
}
