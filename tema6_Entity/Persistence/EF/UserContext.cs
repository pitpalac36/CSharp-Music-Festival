using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Model.domain;

namespace Persistance
{
    public class UserContext : DbContext
    {
        public UserContext(string connectionString) : base(connectionString){}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}