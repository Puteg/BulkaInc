using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Models;
using Bulka.Models.Player;
using Bulka.Repository;
using BulkaBussinessLogic;
using BulkaBussinessLogic.Implementation;

namespace Bulka.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private readonly PlayersRepository _playersRepository;
        private readonly PlayerService _playerService;

        public PlayerController()
        {
            _playersRepository = new PlayersRepository();
            _playerService = new PlayerService(new BulkaContext());
        }

        public ActionResult Profile(int id)
        {
            var player = _playersRepository.FindBy(c => c.Id == id).Single();
            var playerSessions = _playerService.GetSessions(id);

            var playerSessionList = playerSessions.Select(c => new PlayerSessionListViewModel()
            {
                Id = c.Id,
                DateTime = c.Begin.ToLongDateString(),
                Input = c.Input.ToString("0.##"),
                Output = c.Output.ToString("0.##"),
                ClubId = c.ClubId,
                ClubName = c.Club.Name,
                Duration = GetDuration(c.Begin, c.End)
            }).ToList();

            var grouping = playerSessionList.GroupBy(c => c.ClubName).ToList();

            var vm = new PlayerProfileViewModel()
            {
                      Id = player.Id,
                      Name = player.Name,
                      Address = player.Address,
                      Phone = player.Phone,
                      Vk = player.Vk,
                      ImageUrl = player.ImageUrl,
                      AdditionInfo = player.AdditionInfo,

                      PlayerSessionList = playerSessionList,
                      Grouping = grouping
            };

            return View(vm);
        }

        private string GetDuration(DateTime begin, DateTime end)
        {
            var subtract = end.Subtract(begin);
            return string.Format("{0} ч. {1} мин.", (int)subtract.TotalHours, subtract.Minutes);
        }

        public ActionResult All()
        {
            var players = _playersRepository.GetAll().OrderByDescending(c => c.Id).ToList();
            return View(players);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("Edit", new PlayerEditModel(){ImageUrl = "/images/user.png"});
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