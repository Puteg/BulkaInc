using System.Data.Entity;
using Bulka.DataAccess.Migrations;
using Bulka.DataModel;

namespace Bulka.DataAccess
{
    public class BulkaContext : DbContext
    {
        public BulkaContext()
            : base("BulkaContext")
        {
            //Database.SetInitializer(new Configuration());
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<GameProcess> GameProcess { get; set; }
        public DbSet<Account> Accounts { get; set; } 
        public DbSet<PlayerSession> PlayerSessions { get; set; }
    }
}
