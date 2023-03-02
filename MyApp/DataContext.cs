using Microsoft.EntityFrameworkCore;

namespace MyApp
{
    public class DataContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)/mssqllocaldb;Database=DbDataPerson;Trusted_Connection=True;");
        }
    }
}
