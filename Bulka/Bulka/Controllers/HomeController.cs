using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulka.DataAccess;
using Bulka.Models;
using BulkaBussinessLogic.Implementation;

namespace Bulka.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GameProcessService _service;

        public HomeController()
        {
            _service = new GameProcessService(new BulkaContext());
        }

        public ActionResult Index()
        {
            var list = _service.GetAll();
            return View(list);
        }
    }
}