using System.Web.Mvc;
using AutoMapper;
using Bulka.DataAccess;
using Bulka.Models.GameProcess;
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
            var clubs = Mapper.Map<ClubsViewModel>(_service.GetAll());

            var vm = new HomeViewModel()
            {
                ClubList = clubs
            };
            
            return View(vm);
        }
    }
}