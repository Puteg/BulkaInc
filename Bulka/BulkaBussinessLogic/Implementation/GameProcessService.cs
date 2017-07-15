using System;
using System.Linq;
using System.Web.Mvc;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;
using BulkaBussinessLogic.Model.GameProcess;

namespace BulkaBussinessLogic.Implementation
{
    public class GameProcessService : IGameProcessService
    {
        private BulkaContext Context { get; set; }
        private PaymentService PaymentService { get; set; }

        private readonly PlayersRepository _playersRepository;
        private readonly GameProcessRepository _gameProcessRepository;
        private readonly ClubRepository _clubRepository;

        
        public GameProcessService(BulkaContext context)
        {
            Context = context;

             _gameProcessRepository = new GameProcessRepository(context);
            _playersRepository = new PlayersRepository(context);
            _clubRepository = new ClubRepository(context);

            PaymentService = new PaymentService(new PaymentRepository(context));
        }

        public GameProcess Create(int clubId)
        {
            var club = _clubRepository.FindBy(c => c.Id == clubId).First();
            
            var gameProcess = new GameProcess()
            {
                StartDateTime = DateTime.Now,
                Account = new Account(),
                Club = club
            };

            _gameProcessRepository.Add(gameProcess);
            _gameProcessRepository.Save();

            return gameProcess;
        }

        public GameProcessModel Edit(int id)
        {
            var player = _playersRepository.GetAll().ToList();
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == id);

            var gameProcessItems = gameProcess.Payments.Where(c => c.Sender != null).GroupBy(c => c.Sender).Select(c =>
            {
                var gameprocess = new GameProcessItem()
                {
                    PlayerName = c.Key.Name,
                    PlayerImage = c.Key.ImageUrl,
                    Input = c.Select(i => new PlayerStuff()
                    {
                        Amount = i.Amount,
                        Time = i.CreateDateTime.ToShortTimeString()
                    }).ToList(),
                };

                var output = gameProcess.Payments.FirstOrDefault(p => p.Recipient != null && p.Recipient.Id == c.Key.Id);
                if (output != null)
                {
                    gameprocess.OutPut = new PlayerStuff()
                    {
                        Amount = output.Amount,
                        Time = output.CreateDateTime.ToShortTimeString()
                    };
                }

                return gameprocess;
            }).ToList();

            var totalInput = gameProcessItems.SelectMany(c => c.Input.Select(i => i.Amount).ToList()).Sum();
            var totalOutput = gameProcessItems.Where(c => c.OutPut != null).Select(c => c.OutPut.Amount).Sum();
            var vm = new GameProcessModel()
            {
                Items = gameProcessItems,

                DirationTime = DateTime.Now.Subtract(gameProcess.StartDateTime.GetValueOrDefault()).ToString(),
                PlayerCount = gameProcessItems.Count,
                TotalInput = totalInput,
                TotalOutput = totalOutput,
                Total = totalInput - totalOutput,

                Id = id,
                EditModel = new ActionEditModel() {GameProcessId = id},
                Players = player.Select(c => new SelectListItem() {Text = c.Name, Value = c.Id.ToString()}).ToList()
            };

            return vm;
        }

        public void StopProcess(int gameProcessId)
        {
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == gameProcessId);

            gameProcess.EndDateTime = DateTime.Now;
            _gameProcessRepository.Save();
        }

        public bool Seat(int playerId, decimal amount, int gameProcessId)
        {
            var player = _playersRepository.GetAll().First(c => c.Id == playerId);
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == gameProcessId);

            PaymentService.Transfer(player.Account, gameProcess.Account, amount, gameProcessId, player);
            return true;
        }

        public bool Rebuy(int playerId, decimal amount, int gameProcessId)
        {
            var player = _playersRepository.GetAll().First(c => c.Id == playerId);
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == gameProcessId);

            PaymentService.Transfer(player.Account, gameProcess.Account, amount, gameProcessId, player);
            return true;
        }

        public bool SeatOut(int playerId, decimal amount, int gameProcessId)
        {
            var player = _playersRepository.GetAll().First(c => c.Id == playerId);
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == gameProcessId);

            PaymentService.Transfer(gameProcess.Account, player.Account, amount, gameProcessId, null, player);
            return true;
        }

        public GameProcessList GetAll()
        {
            var list = new GameProcessList();

            var gameProcesses = _gameProcessRepository.GetAll().ToList();

            foreach (var gameProcess in gameProcesses)
            {
                var gameProcessItems = gameProcess.Payments.Where(c => c.Sender != null).GroupBy(c => c.Sender).Select(c =>
                {
                    var gameprocess = new GameProcessItem()
                    {
                        PlayerName = c.Key.Name,
                        PlayerImage = c.Key.ImageUrl,
                        Input = c.Select(i => new PlayerStuff()
                        {
                            Amount = i.Amount,
                            Time = i.CreateDateTime.ToShortTimeString()
                        }).ToList(),
                    };

                    var output = gameProcess.Payments.FirstOrDefault(p => p.Recipient != null && p.Recipient.Id == c.Key.Id);
                    if (output != null)
                    {
                        gameprocess.OutPut = new PlayerStuff()
                        {
                            Amount = output.Amount,
                            Time = output.CreateDateTime.ToShortTimeString()
                        };
                    }

                    return gameprocess;
                }).ToList();

                var totalInput = gameProcessItems.SelectMany(c => c.Input.Select(i => i.Amount).ToList()).Sum();
                var totalOutput = gameProcessItems.Where(c => c.OutPut != null).Select(c => c.OutPut.Amount).Sum();

                var item = new GameProcessListItem()
                {
                    Id = gameProcess.Id,
                    DateTime = gameProcess.StartDateTime.GetValueOrDefault().ToShortDateString(),
                    DirationTime = DateTime.Now.Subtract(gameProcess.StartDateTime.GetValueOrDefault()).TotalMinutes.ToString(),
                    PlayerCount = gameProcessItems.Count,
                    TotalInput = totalInput,
                    TotalOutput = totalOutput,
                    Total = totalInput - totalOutput,
                };
                list.Items.Add(item);
            }

            return list;
        }
    }
}
 