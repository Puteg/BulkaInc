using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataModel;

namespace BulkaBussinessLogic
{
    public interface IPlayerService
    {
        Player Create(string firstName, string phone);
    }
}
