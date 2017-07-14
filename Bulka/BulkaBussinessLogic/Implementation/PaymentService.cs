using Bulka.DataModel;

namespace BulkaBussinessLogic.Implementation
{
    public class PaymentService : IPaymentService
    {
        public bool Transfer(Account sender, Account recipient, decimal amount)
        {
            sender.Balance -= amount;
            recipient.Balance += amount;
            
            return true;
        }
    }
}
