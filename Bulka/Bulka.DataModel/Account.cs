using System;
using Bulka.DataModel.Enum;

namespace Bulka.DataModel
{
    public class Account
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }

        public decimal Balance { get; set; }
    }
}
