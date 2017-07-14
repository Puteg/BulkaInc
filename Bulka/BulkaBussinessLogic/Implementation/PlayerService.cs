using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.DataModel.Enum;

namespace BulkaBussinessLogic.Implementation
{
    public class PlayerService : IPlayerService
    {
        private BulkaContext Context { get; set; }

        public PlayerService(BulkaContext context)
        {
            this.Context = context;
        }

        public Player Create(string firstName, string phone)
        {
            var newAccount = new Account()
            {
                Type = AccountType.Virtual
            };

            var newPlayer = new Player()
            {
                Name = firstName,
                Phone = phone,
                Account = newAccount
            };

            Context.Accounts.Add(newAccount);
            Context.Players.Add(newPlayer);

            Context.SaveChanges();

            return newPlayer;
        }
    }
}
