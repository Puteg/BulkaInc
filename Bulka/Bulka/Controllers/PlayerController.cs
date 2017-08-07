using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Models;
using Bulka.Models.GameProcess;
using Bulka.Models.Player;
using Bulka.Repository;
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
            var grouping = playerSessions.GroupBy(c => c.Club.Name).OrderBy(c => c.Key).ToList();
            var vm = Mapper.Map<PlayerProfileViewModel>(player);

            if (playerSessions.Any())
            {
                var all = Mapper.Map<PlayerSessionListViewModel>(playerSessions);
                all.Title = "Все игры";

                var groupingSessions = grouping.Select(g =>
                {
                    var tmp = Mapper.Map<PlayerSessionListViewModel>(g.ToList());
                    tmp.Title = g.Key;

                    return tmp;
                }).ToList();

                vm.Sessions.Add(all);
                vm.Sessions.AddRange(groupingSessions);
                vm.Visitation = new PlayerVisitation
                {
                    VisitationItems = grouping.Select(c => new PlayerVisitationItem()
                    {
                        ClubName = c.Key,
                        Persent = ((int) ((double) c.Count()/playerSessions.Count*100)).ToString()
                    }).ToList()
                };
            }
            return View(vm);
        }

        public ActionResult All()
        {
            var players = _playersRepository.GetAll().OrderByDescending(c => c.Id).ToList();
            return View(players);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("Edit", new PlayerEditModel() {ImageUrl = "/images/user.png"});
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
                return RedirectToAction("Profile", new {@id = player.Id});
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var player = _playersRepository.FindBy(c => c.Id == id).Single();
            _playersRepository.Delete(player);
            _playersRepository.Save();

            return RedirectToAction("All");
        }

        public JsonResult PlayerSerch(string query)
        {
            var players = _playerService.Search(query);
            var playersViewModel = Mapper.Map<List<PlayerItemViewItem>>(players);
            var jsonData = playersViewModel.Select(c => new
            {
                id = c.Id, 
                text = c.Text, 
                phone = c.Phone,
                image = c.ImageUrl
            });
              
            return Json(data: jsonData, behavior: JsonRequestBehavior.AllowGet);
        }
    }
}