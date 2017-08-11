using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bulka.DataModel;
using Bulka.Repository;

namespace BulkaBussinessLogic.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;
        
        public DateTime DateTime { get; set; }

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

            DateTime = DateTime.Now;
        }

        public bool Transfer(Account sender, Account recipient, decimal amount, int? gameProcessId = null, Player senderPlayer = null, Player recipientPlayer = null)
        {
            sender.Balance -= amount;
            recipient.Balance += amount;

            var payment = new Payment
            {
                CreateDateTime = DateTime,
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

        public List<Payment> GetAll()
        {
            var payments = _paymentRepository.GetAll().OrderByDescending(c => c.Id).Take(100).ToList();
            return payments;
        }

        public Payment Get(int id)
        {
            var payment = _paymentRepository.GetAll().First(c => c.Id == id);
            return payment;
        }

        public bool Edit(Payment edit)
        {
            var payment = _paymentRepository.GetAll().First(c => c.Id == edit.Id);

            Mapper.Map(edit, payment);

            _paymentRepository.Save();
            return true;
        }

        public void Delete(int id)
        {
            var club = _paymentRepository.GetAll().First(c => c.Id == id);
            _paymentRepository.Delete(club);
            _paymentRepository.Save();
        }
    }
}
