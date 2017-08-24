using AspNetCoreWebAPI.Entities;

namespace AspNetCoreWebAPI.Services
{
    public class GenericEFRepository : IGenericEFRepository
    {
        private SqlDbContext _db;
        public GenericEFRepository(SqlDbContext db)
        {
            _db = db;
        }


    }
}
