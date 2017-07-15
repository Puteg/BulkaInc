using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class ClubRepository : GenericRepository<BulkaContext, Club>
    {
        public ClubRepository()
        {

        }

        public ClubRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
