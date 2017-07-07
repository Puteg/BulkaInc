using System.Data.Entity;
using Bulka.DataModel;

namespace Bulka.DataAccess
{
    public class BulkaContext : DbContext 
    {
        public BulkaContext()
            : base("BulkaContext")
        {
            
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<GameProcess> GameProcess { get; set; }
        public DbSet<Account> Accounts { get; set; } 
    }
}
