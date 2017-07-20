using System.Data.Entity;
using Bulka.DataModel;

namespace Bulka.DataAccess
{
    public class BulkaContext : DbContext
    {
        public BulkaContext()
            : base("BulkaContext")
        {
            Database.SetInitializer(new NullDatabaseInitializer<BulkaContext>());
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<GameProcess> GameProcess { get; set; }
        public DbSet<Account> Accounts { get; set; } 
        public DbSet<PlayerSession> PlayerSessions { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("dbo.AspNetUsers");
        }
    }
}
