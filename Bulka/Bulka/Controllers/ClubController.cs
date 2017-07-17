using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulka.DataAccess;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.Club;

namespace Bulka.Controllers
{
    public class ClubController : Controller
    {
        private readonly ClubService _clubService;

        public ClubController()
        {
            _clubService = new ClubService(new BulkaContext());
        }

        public ActionResult Index()
        {
            var vm = _clubService.GetAll();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            var vm = _clubService.Get(id);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ClubEdit model)
        {
            if (ModelState.IsValid)
            {
                if (_clubService.Edit(model))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _clubService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}