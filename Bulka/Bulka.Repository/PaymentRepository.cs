using Bulka.DataAccess;
using Bulka.DataModel;

namespace Bulka.Repository
{
    public class PaymentRepository : GenericRepository<BulkaContext, Payment>
    {
        public PaymentRepository()
        {

        }

        public PaymentRepository(BulkaContext context)
        {
            this.Context = context;
        }
    }
}
