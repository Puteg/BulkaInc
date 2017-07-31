using System.Web.Mvc;
using AutoMapper;
using Bulka.DataAccess;
using Bulka.Models.Club;
using Bulka.Models.Club.EditModel;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.Club;

namespace Bulka.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        private readonly ClubService _clubService;

        public ClubController()
        {
            _clubService = new ClubService(new BulkaContext());
        }

        public ActionResult Index()
        {
            var clubList = _clubService.GetAll();
            var vm = Mapper.Map<ClubsViewModel>(clubList);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            var edit = _clubService.Get(id);
            var editVm = Mapper.Map<ClubEditModel>(edit);

            return View(editVm);
        }

        [HttpPost]
        public ActionResult Edit(ClubEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<ClubEdit>(editModel);
                if (_clubService.Edit(model))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(editModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _clubService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}