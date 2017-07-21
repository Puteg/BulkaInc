using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class PlayerSessionRepository : GenericRepository<BulkaContext, PlayerSession>
    {
        public PlayerSessionRepository()
        {

        }

        public PlayerSessionRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
