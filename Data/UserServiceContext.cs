using Microsoft.EntityFrameworkCore;

namespace entityFramework.Data
{
    public class UserServiceContext : DbContext
    {
        private readonly string _connectionString;

        public UserServiceContext()
        {
        }

        public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
        {
        }

        public UserServiceContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Models.User> User { get; set; }

    }
}