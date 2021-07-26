using entityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace entityFramework.Data
{
    public class UserDataAccess
    {
        private DbContextOptions<UserServiceContext> _read;
        private DbContextOptions<UserServiceContext> _write;
        private IConfiguration Configuration { get; }

        public UserDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<Models.User>>> GetById(int id)
        {
            var dbContext = new UserServiceContext(this.GetReadContextOptions(false));

            if (!dbContext.Database.CanConnect())
                dbContext = new UserServiceContext(this.GetReadContextOptions(true));

            using (dbContext)
            {
                return await dbContext.User.Where(p => p.Id == id).ToListAsync();
            }
        }

        public async Task<int> CreateUser(User item)
        {
            var dbContext = new UserServiceContext(this.GetWriteContextOptions(false));

            if (!dbContext.Database.CanConnect())
                dbContext = new UserServiceContext(this.GetWriteContextOptions(true));

            using (dbContext)
            {
                dbContext.User.Add(item);

                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateUser(User item)
        {
            var dbContext = new UserServiceContext(this.GetWriteContextOptions(false));

            if (!dbContext.Database.CanConnect())
                dbContext = new UserServiceContext(this.GetWriteContextOptions(true));

            using (dbContext)
            {
                var objEdited = await dbContext.User.Where(p => p.Id == item.Id).FirstOrDefaultAsync();

                if (objEdited != null)
                {
                    objEdited.Address = item.Address;
                    objEdited.Email = item.Email;
                    objEdited.Name= item.Name;
                    objEdited.Updated = DateTime.Now;
                }

                return await dbContext.SaveChangesAsync();
            }
        }
        private DbContextOptions<UserServiceContext> GetReadContextOptions(bool force)
        {
            if (_read is null || force)
                _read = this.GetOptionsBuilder("read").Options;

            return _read;
        }

        private DbContextOptions<UserServiceContext> GetWriteContextOptions(bool force)
        {
            if (_write is null || force)
                _write = this.GetOptionsBuilder("write").Options;

            return _write;
        }
        private DbContextOptionsBuilder<UserServiceContext> GetOptionsBuilder(string strConnectionProfile)
        {
            //***strConnectionProfile***  il faut creer l'autre BD pour changer la connstring entre READ et WRITE mode
            return new DbContextOptionsBuilder<UserServiceContext>().UseNpgsql(new PostgresConnection(Configuration).GetConnectionString);
        }

    }
}