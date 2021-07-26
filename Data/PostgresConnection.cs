using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace entityFramework.Data
{
    public class PostgresConnection
    {
        public string GetConnectionString { get; set; }
        private IConfiguration Configuration { get; set; }
        
        public PostgresConnection(IConfiguration configuration)
        {
            Configuration = configuration;
            this.GetPostgresConnectionString();
        }

        private void GetPostgresConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Configuration.GetValue<string>("db_host"),
                Database = Configuration.GetValue<string>("db_database"),
                Username = Configuration.GetValue<string>("db_username"),
                Password = Configuration.GetValue<string>("db_password")
            };

            GetConnectionString = builder.ConnectionString;
        }

    }
}