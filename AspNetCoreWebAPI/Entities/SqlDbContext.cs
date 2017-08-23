using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebAPI.Entities
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
        {
        }
    }
}
