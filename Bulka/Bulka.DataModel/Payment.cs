using System;

namespace Bulka.DataModel
{
    public class Payment
    {
        public long Id { get; set; }
        public int? GameProcessId { get; set; }
        public Guid SenderAccountId { get; set; }
        public Guid RecipientAccountId { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime CreateDateTime { get; set; }

        public virtual Player Sender { get; set; }
        public virtual Player Recipient { get; set; }
        public virtual GameProcess GameProcess { get; set; }
    }
}
