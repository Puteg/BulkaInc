using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bulka.Models;
using Bulka.Repository;

namespace Bulka.Controllers
{
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
            var players = _playersRepository.GetAll().ToList();
            return View(players);
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
                var player = _playersRepository.FindBy(c => c.Id == model.Id).Single();

                player.Phone = model.Phone;
                player.Name = model.Name;
                player.ImageUrl = model.ImageUrl;
                player.Address = model.Address;
                player.Vk = model.Vk;
                player.AdditionInfo = model.AdditionInfo;

                _playersRepository.Save();
                return RedirectToAction("Profile", new { @id = model.Id});
            }

            return View(model);
        }
    }
}