using Microsoft.EntityFrameworkCore;
using SearchEngine.DataBase.Model;
using System;

namespace SearchEngine.DataBase
{
    public class SearchEngineContext : DbContext
    {
        public SearchEngineContext()
        {
        }

        public SearchEngineContext(DbContextOptions<SearchEngineContext> options)
            : base(options)
        {

        }

        public DbSet<LinkPositionTracker> LinkTracker { get; set; }
        public DbSet<PositonAndDate> PositonAndDates { get; set; }
        public DbSet<LinkDetails> LinkDetails { get; set; }
        public DbSet<Keywords> Keywords { get; set; }
        public DbSet<ExternalLinks> ExternalLinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\ADMINISTRATOR\\KIRIL\\SEARCHENGINE.MDF;Integrated Security=True;",
                 sqlServerOptionsAction: sqlAction =>
                 {
                     sqlAction.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                         );
                 });
               
            base.OnConfiguring(builder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LinkPositionTracker>()
                .HasKey(k => new { k.Keywords, k.Link});
        }
    }
}
