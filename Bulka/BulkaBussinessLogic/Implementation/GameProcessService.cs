using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataAccess;
using Bulka.DataModel;

namespace BulkaBussinessLogic.Implementation
{
    public class GameProcessService : IGameProcessService
    {
        private BulkaContext Contex { get; set; }
        private GameProcess GameProcess { get; set; }
        private PaymentService PaymentService { get; set; }
        private Account Account { get; set; }

        public GameProcessService(BulkaContext contex, Club club)
        {
            Contex = contex;

            Account = new Account();
            contex.Accounts.Add(Account);
            contex.SaveChanges();

            GameProcess = new GameProcess { Club = club, AccountId = Account.Id };
        }

        public void StartProcess()
        {
            GameProcess.StartDateTime = DateTime.Now;
        }
        
        public void StopProcess()
        {
            GameProcess.EndDateTime = DateTime.Now;
        }

        public bool Seat(Player player, decimal amount)
        {
            PaymentService.Transfer(player.Account, Account, amount);
            return true;
        }

        public bool Rebuy(Player player, decimal amount)
        {
            PaymentService.Transfer(player.Account, Account, amount);
            return true;
        }

        public bool SeatOut(Player player, decimal amount)
        {
            PaymentService.Transfer(Account, player.Account, amount);
            return true;
        }
    }
}
 