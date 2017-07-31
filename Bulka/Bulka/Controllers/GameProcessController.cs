using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Bulka.DataAccess;
using Bulka.Models.GameProcess;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.GameProcess;

namespace Bulka.Controllers
{
    [Authorize]
    public class GameProcessController : Controller
    {
        private readonly GameProcessService _processService;
        private readonly ClubService _clubService;

        public GameProcessController()
        {
            var contex = new BulkaContext();
            _processService = new GameProcessService(contex);
            _clubService = new ClubService(contex);
        }

        public ActionResult Create(int clubId)
        {
            var gameProcess = _processService.Create(clubId);
            return RedirectToAction("Edit", new { id = gameProcess.Id });
        }

        public ActionResult Details(int id)
        {
            var processModel = _processService.Edit(id);
            var vm = Mapper.Map<GameProcessEditModel>(processModel);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var processModel = _processService.Edit(id);
            var vm = Mapper.Map<GameProcessEditModel>(processModel);

            return View(vm);
        }

        public ActionResult End(int id)
        {
            _processService.StopProcess(id);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult CreateTest()
        {
            var clubs = _clubService.GetAll();
            var vm = new TestGameProcessViewModel()
            {
                DateTime = DateTime.Now,
                ClubSelectListItems = clubs.Items.Select(c => new SelectListItem() {Value = c.Id.ToString(), Text = c.Name}).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateTest(TestGameProcessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var testGameProcessResquest = Mapper.Map<TestGameProcessResquest>(model);
                _processService.CreateTestGameProcess(testGameProcessResquest);
            }

            return View(model);
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
                            _processService.Seat(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                        case ActionType.Rebuy:
                            _processService.Rebuy(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                        case ActionType.SeatOut:
                            _processService.SeatOut(model.PlayerId, model.Amount, model.GameProcessId);
                            break;
                    }
                }
            }

            return RedirectToAction("Edit", new { id = model.GameProcessId });
        }
    }
}