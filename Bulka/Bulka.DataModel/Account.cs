using System;
using Bulka.DataModel.Enum;

namespace Bulka.DataModel
{
    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
            Type = AccountType.Virtual;
            Balance = 0;
        }

        public Guid Id { get; set; }
        public AccountType Type { get; set; }

        public decimal Balance { get; set; }
    }
}
