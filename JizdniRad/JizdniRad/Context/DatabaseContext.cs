using JizdniRad.Models;
using Microsoft.EntityFrameworkCore;

namespace JizdniRad.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Departure> departures { get; set; }
        public DbSet<LineStop> lineStops { get; set; }
        public DbSet<Line> lines { get; set; }
        public DbSet<Stop> stops { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

#warning Please update the connection string with your own database credentials.
          //Put connection string here
            optionsBuilder.UseMySQL("<connection_string>")
                ;//.UseLazyLoadingProxies(); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
        }

    }
}
