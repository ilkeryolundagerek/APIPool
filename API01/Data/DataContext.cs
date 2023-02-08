using API01.Data.Configurations;
using API01.Entities;
using Microsoft.EntityFrameworkCore;


namespace API01.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Context kullanılırken içinde bulunan enitity yapılarının ve db tarafındaki tabloları yapılandırmaları hakkında bilgi taşır.

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
    }
}
