using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class PlayersRepository : GenericRepository<BulkaContext, Player>
    {
        public PlayersRepository()
        {
            
        }

        public PlayersRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
