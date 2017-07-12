using System.Linq;
using System.Net;
using System.Web.Mvc;
using Bulka.DataModel;
using Bulka.Models;
using Bulka.Repository;

namespace Bulka.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private readonly PlayersRepository _playersRepository;

        public PlayerController()
        {
            _playersRepository = new PlayersRepository();
        }

        public ActionResult Profile(int id)
        {
            var player = _playersRepository.FindBy(c => c.Id == id).Single();
            return View(player);
        }

        public ActionResult All()
        {
            var players = _playersRepository.GetAll().OrderByDescending(c => c.Id).ToList();
            return View(players);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("Edit", new PlayerEditModel());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = _playersRepository.FindBy(c => c.Id == id).Single();
            if (player == null)
            {
                return HttpNotFound();
            }

            PlayerEditModel vm = new PlayerEditModel
            {
                Id = player.Id,
                Phone = player.Phone,
                Name = player.Name,
                ImageUrl = player.ImageUrl,
                Address = player.Address,
                Vk = player.Vk,
                AdditionInfo = player.AdditionInfo
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var isNew = model.Id == 0;
                Player player;

                if (isNew)
                {
                    player = new Player
                    {
                        Account = new Account()
                    };
                }
                else
                {
                    player = _playersRepository.FindBy(c => c.Id == model.Id).Single();
                }

                player.Phone = model.Phone;
                player.Name = model.Name;
                player.ImageUrl = model.ImageUrl;
                player.Address = model.Address;
                player.Vk = model.Vk;
                player.AdditionInfo = model.AdditionInfo;

                if (isNew)
                {
                    _playersRepository.Add(player);
                }

                _playersRepository.Save();
                return RedirectToAction("Profile", new { @id = player.Id});
            }

            return View(model);
        }
    }
}