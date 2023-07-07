using Microsoft.EntityFrameworkCore;

namespace MyApp.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-GJ5HJ6I;Database=DbDataPerson;Trusted_Connection=True;");
        }
    }
}
