using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CarparkApi.Models;

namespace CarparkApi.Util {
    public class DataContext : DbContext {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration) {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseMySQL(Configuration.GetConnectionString("Default"));
        }

        public DbSet<User> Users { get; set; }
    }
}