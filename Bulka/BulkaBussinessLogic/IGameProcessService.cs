using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataModel;

namespace BulkaBussinessLogic
{
    public interface IGameProcessService
    {
        void StartProcess();
        void StopProcess();

        bool Seat(Player player, decimal amount);
        bool Rebuy(Player player, decimal amount);
        bool SeatOut(Player player, decimal amount);
    }
}
