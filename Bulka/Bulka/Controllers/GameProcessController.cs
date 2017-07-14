using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulka.DataModel;
using Bulka.Models;
using Bulka.Repository;

namespace Bulka.Controllers
{
    public class GameProcessController : Controller
    {
        private readonly GameProcess _gameProcess;
        private readonly PlayersRepository _playersRepository;
        private readonly PaymentRepository _paymentRepository;

        public GameProcessController()
        {
            var gameProcessRepository = new GameProcessRepository();

            _gameProcess = gameProcessRepository.GetAll().FirstOrDefault();
            if (_gameProcess == null)
            {
                _gameProcess = new GameProcess();
                gameProcessRepository.Add(_gameProcess);
                gameProcessRepository.Save();
            }
            
            _playersRepository = new PlayersRepository();
            _paymentRepository = new PaymentRepository();
        }

        // GET: GameProcess
        public ActionResult Index(int id)
        {
            var player = _playersRepository.GetAll().ToList();

            var vm = new GameProcessModel()
            {
                EditModel = new ActionEditModel(){ GameProcessId = id },
                Players = player.Select(c => new SelectListItem(){Text = c.Name, Value = c.Id.ToString()}).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Action(ActionEditModel model)
        {
            if (TryUpdateModel(model, "EditModel"))
            {
                if (ModelState.IsValid)
                {
                    Payment payment = null;
                    var player = _playersRepository.FindBy(c => c.Id == model.PlayerId).First();

                    switch (model.Type)
                    {
                        case ActionType.Seat:
                        case ActionType.Rebuy:

                            payment = new Payment
                            {
                                CreateDateTime = DateTime.Now,
                                Amount = model.Amount,
                                RecipientAccountId = _gameProcess.AccountId,
                                SenderAccountId = player.AccountId
                            };

                            break;
                        case ActionType.SeatOut:
                            payment = new Payment
                            {
                                CreateDateTime = DateTime.Now,
                                Amount = model.Amount,
                                RecipientAccountId = player.AccountId,
                                SenderAccountId = _gameProcess.AccountId,
                            };
                            break;
                    }
                    _paymentRepository.Add(payment);
                    _paymentRepository.Save();
                }
            }

            return RedirectToAction("Index", new { id = model.GameProcessId });
        }
    }
}