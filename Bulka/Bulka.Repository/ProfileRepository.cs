using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class ProfileRepository : GenericRepository<BulkaContext, Profile>
    {
        public ProfileRepository()
        {

        }

        public ProfileRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
