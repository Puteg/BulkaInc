using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class GameProcessRepository : GenericRepository<BulkaContext, GameProcess>
    {
        public GameProcessRepository()
        {

        }

        public GameProcessRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
