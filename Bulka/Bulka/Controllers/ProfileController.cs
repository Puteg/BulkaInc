using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulka.DataAccess;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.Profile;
using Microsoft.AspNet.Identity;

namespace Bulka.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;

        public ProfileController()
        {
            _profileService = new ProfileService(new BulkaContext());
        }

        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var vm = _profileService.Get(userId);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var vm = _profileService.GetEdit(userId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEdit model)
        {
            if (ModelState.IsValid)
            {
                var result = _profileService.Edit(model);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}