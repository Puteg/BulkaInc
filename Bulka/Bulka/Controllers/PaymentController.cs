using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;
using BulkaBussinessLogic.Implementation;

namespace Bulka.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController()
        {
            var paymentRepository = new PaymentRepository(new BulkaContext());
            _paymentService = new PaymentService(paymentRepository);
        }

        public ActionResult Index()
        {
            var payments = _paymentService.GetAll();
            var paymentsVm = Mapper.Map<List<PaymentItemViewModel>>(payments);

            var vm = new PaymentsViewModel { Items = paymentsVm };
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _paymentService.Get(id);
            var vm = Mapper.Map<PaymentEditModel>(model);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                var model = _paymentService.Get(editModel.Id);
                
                Mapper.Map(editModel, model);
                
                if (_paymentService.Edit(model))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(editModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _paymentService.Delete(id);
            return RedirectToAction("Index");
        }
    }

    public class PaymentEditModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }

        public string From { get; set; }
        public string To { get; set; }
    }

    public class PaymentsViewModel
    {
        public List<PaymentItemViewModel> Items { get; set; } 
    }

    public class PaymentItemViewModel
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string Amount { get; set; }

        public string Sender { get; set; }
        public string Recipient { get; set; }
    }
}