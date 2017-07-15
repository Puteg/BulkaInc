using System;
using Bulka.DataModel;
using Bulka.Repository;

namespace BulkaBussinessLogic.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool Transfer(Account sender, Account recipient, decimal amount, int? gameProcessId = null, Player senderPlayer = null, Player recipientPlayer = null)
        {
            sender.Balance -= amount;
            recipient.Balance += amount;

            var payment = new Payment
            {
                CreateDateTime = DateTime.Now,
                Amount = amount,
                RecipientAccountId = recipient.Id,
                SenderAccountId = sender.Id,

                Sender = senderPlayer,
                Recipient = recipientPlayer,
                GameProcessId = gameProcessId
            };

            _paymentRepository.Add(payment);
            _paymentRepository.Save();

            return true;
        }
    }
}
