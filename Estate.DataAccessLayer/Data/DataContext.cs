using Estate.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estate.DataAccessLayer.Data
{
    public class DataContext:IdentityDbContext<UserAdmin>
    {
        public DataContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server  =DESKTOP-0TJT8G1; Database = Estate; Integrated Security = True;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DataContext(DbContextOptions options) : base(options)
        { 

        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<Estate.EntityLayer.Entities.Type> Types { get; set; }
        public DbSet<UserAdmin> UserAdmin { get; set; }

    }
}
