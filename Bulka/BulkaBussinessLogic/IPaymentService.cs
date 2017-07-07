using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataModel;

namespace BulkaBussinessLogic
{
    public interface IPaymentService
    {
        bool Transfer(Account sender, Account recipient, decimal amount);
    }
}
