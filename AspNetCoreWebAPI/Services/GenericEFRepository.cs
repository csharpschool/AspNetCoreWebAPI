using AspNetCoreWebAPI.Entities;
using System.Collections.Generic;

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

    }
}
