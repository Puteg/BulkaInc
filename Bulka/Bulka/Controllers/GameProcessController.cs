using System.Web.Mvc;
using Bulka.DataAccess;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.GameProcess;

namespace Bulka.Controllers
{
    public class GameProcessController : Controller
    {
        private readonly GameProcessService _service;

        public GameProcessController()
        {
            _service = new GameProcessService(new BulkaContext());
        }

        [HttpPost]
        public ActionResult Action(ActionEditModel model)
        {
            if (TryUpdateModel(model, "EditModel"))
            {
                if (ModelState.IsValid)
                {

                    switch (model.Type)
                    {
                        case ActionType.Seat:
                            _service.Seat(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                        case ActionType.Rebuy:
                            _service.Rebuy(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                        case ActionType.SeatOut:
                            _service.SeatOut(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                    }
                }
            }

            return RedirectToAction("Edit", new { id = model.GameProcessId });
        }

        public ActionResult Details(int id)
        {
            var vm = _service.Edit(id);
            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.Edit(id);
            return View(vm);
        }

        public ActionResult Create(int clubId)
        {
            var gameProcess = _service.Create(clubId);
            return RedirectToAction("Edit", new { id = gameProcess.Id });
        }

        public ActionResult End(int id)
        {
            _service.StopProcess(id);

            return RedirectToAction("Details", new { id = id });
        }
    }
}