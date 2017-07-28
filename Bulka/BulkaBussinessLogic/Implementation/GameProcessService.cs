using System;
using System.Collections.Generic;
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
        private PaymentService PaymentService { get; set; }
        private PlayerSessionService PlayerSessionService { get; set; }

        private readonly PlayersRepository _playersRepository;
        private readonly GameProcessRepository _gameProcessRepository;
        private readonly ClubRepository _clubRepository;

        public DateTime DateTime { get; set; }

        public GameProcessService(BulkaContext context)
        {
            _gameProcessRepository = new GameProcessRepository(context);
            _playersRepository = new PlayersRepository(context);
            _clubRepository = new ClubRepository(context);

            PaymentService = new PaymentService(new PaymentRepository(context));
            PlayerSessionService = new PlayerSessionService(context);

            DateTime = DateTime.Now;
        }

        public GameProcess Create(int clubId)
        {
            var club = _clubRepository.FindBy(c => c.Id == clubId).First();
            
            var gameProcess = new GameProcess
            {
                StartDateTime = DateTime,
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

            var gameProcessItems = GetGameProcessItems(gameProcess);

            var totalInput = gameProcessItems.SelectMany(c => c.Input.Select(i => i.Amount).ToList()).Sum();
            var totalOutput = gameProcessItems.Where(c => c.OutPut != null).Select(c => c.OutPut.Amount).Sum();

            TimeSpan subtract;
            subtract = !gameProcess.EndDateTime.HasValue
                ? DateTime.Subtract(gameProcess.StartDateTime.GetValueOrDefault())
                : gameProcess.EndDateTime.Value.Subtract(gameProcess.StartDateTime.GetValueOrDefault());
            var dirationTimeStr = string.Format("{0} ч. {1} мин.", (int)subtract.TotalHours, subtract.Minutes);

            var vm = new GameProcessModel
            {
                Items = gameProcessItems,

                DirationTime = dirationTimeStr,
                PlayerCount = gameProcessItems.Count,
                TotalInput = totalInput.ToString("0.##"),
                TotalOutput = totalOutput.ToString("0.##"),
                Total = (totalInput - totalOutput).ToString("0.##"),

                Id = id,
                EditModel = new ActionEditModel {GameProcessId = id},
                Players = player.Select(c => new SelectListItem {Text = c.Name, Value = c.Id.ToString()}).ToList()
            };

            return vm;
        }

        public void StopProcess(int gameProcessId)
        {
            var gameProcess = _gameProcessRepository.GetAll().First(c => c.Id == gameProcessId);

            if (!gameProcess.EndDateTime.HasValue)
            {
                gameProcess.EndDateTime = DateTime;
                _gameProcessRepository.Save();

                var playersSessions = new List<PlayerSession>();

                var gameProcessItems = GetGameProcessItems(gameProcess);

                foreach (var gameProcessItem in gameProcessItems)
                {
                    if (gameProcessItem.OutPut == null)
                    {
                        gameProcessItem.OutPut = new PlayerStuff
                        {
                            Amount = 0,
                            Time = DateTime
                        };
                    }

                    playersSessions.Add(new PlayerSession
                    {
                        PlayerId = gameProcessItem.PlayerId,
                        ClubId = gameProcess.ClubId,
                        Begin = gameProcessItem.Input.First().Time,
                        End = gameProcessItem.OutPut.Time,
                        Input = gameProcessItem.Input.Sum(c => c.Amount),
                        Output = gameProcessItem.OutPut.Amount
                    });
                }

                PlayerSessionService.Create(playersSessions);
            }
            
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

        public ClubList GetAll()
        {
            var clubs = _clubRepository.GetAll().ToList();
            var gameProcessList = new ClubList();

            foreach (var club in clubs)
            {
                var clubItem = new ClubItem
                {
                    Id = club.Id,
                    Name = club.Name
                };

                var gameProcesses = _gameProcessRepository.GetAll().Where(c => c.ClubId == club.Id).ToList();

                foreach (var gameProcess in gameProcesses)
                {
                    var gameProcessItems = GetGameProcessItems(gameProcess);

                    var totalInput = gameProcessItems.SelectMany(c => c.Input.Select(i => i.Amount).ToList()).Sum();
                    var totalOutput = gameProcessItems.Where(c => c.OutPut != null).Select(c => c.OutPut.Amount).Sum();

                    TimeSpan subtract;
                    subtract = !gameProcess.EndDateTime.HasValue
                        ? DateTime.Subtract(gameProcess.StartDateTime.GetValueOrDefault())
                        : gameProcess.EndDateTime.Value.Subtract(gameProcess.StartDateTime.GetValueOrDefault());
                    var dirationTimeStr = string.Format("{0} ч. {1} мин.", (int)subtract.TotalHours, subtract.Minutes);

                    var item = new GameProcessListItem
                    {
                        Id = gameProcess.Id,
                        DateTime = gameProcess.StartDateTime.GetValueOrDefault().ToShortDateString(),
                        DirationTime = dirationTimeStr,
                        PlayerCount = gameProcessItems.Count,
                        TotalInput = totalInput.ToString("0.##"),
                        TotalOutput = totalOutput.ToString("0.##"),
                        Total = (totalInput - totalOutput)
                    };
                    clubItem.Items.Add(item);
                }

                clubItem.PlayersCount = clubItem.Items.Sum(c => c.PlayerCount);
                clubItem.Total = clubItem.Items.Sum(c => c.Total);

                clubItem.Items = clubItem.Items.OrderByDescending(c => c.DateTime).ToList();
                gameProcessList.Clubs.Add(clubItem);
            }

            return gameProcessList;
        }

        public bool CreateTestGameProcess(TestGameProcessResquest request)
        {
            var rand = new Random();
            var players = new List<Player>();
            var allPlayers = _playersRepository.GetAll().ToList();

            for (int j = 0; j < request.DayCount; j++)
            {
                for (int i = 0; i < request.PlayersCount; i++)
                {
                    players.Add(allPlayers.ElementAt(rand.Next(0, allPlayers.Count)));
                }

                var dateTime = request.DateTime.AddDays(j);

                UpdateDateTime(dateTime);

                var gameProcess = Create(request.ClubId);

                foreach (var player in players)
                {
                    Seat(player.Id, rand.Next(request.MinAmount, request.MaxAmount), gameProcess.Id);
                }

                foreach (var player in players)
                {
                    var avgCount = rand.Next(0, request.AvgCountRebuy);
                    for (int i = 0; i < avgCount; i++)
                    {
                        dateTime = dateTime.AddMinutes(rand.Next(10, 25));
                        UpdateDateTime(dateTime);
                        Rebuy(player.Id, rand.Next(request.MinAmount, request.MaxAmount), gameProcess.Id);
                    }
                }

                foreach (var player in players)
                {
                    dateTime = dateTime.AddMinutes(rand.Next(10, 35));
                    UpdateDateTime(dateTime);
                    var isOut = rand.Next(0, 100) < 45;
                    if (isOut)
                    {
                        SeatOut(player.Id, rand.Next(request.MinAmount, request.MaxAmount*2), gameProcess.Id);
                    }
                }

                StopProcess(gameProcess.Id);
            }
            return true;
        }

        private void UpdateDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
            PaymentService.DateTime = dateTime;   
        }

        private List<GameProcessItem> GetGameProcessItems(GameProcess gameProcess)
        {
            var gameProcessItems = gameProcess.Payments.Where(c => c.Sender != null).GroupBy(c => c.Sender).Select(c =>
            {
                var gameprocess = new GameProcessItem
                {
                    PlayerId = c.Key.Id,
                    PlayerName = c.Key.Name,
                    PlayerImage = c.Key.ImageUrl,
                    
                    Input = c.Select(i => new PlayerStuff
                    {
                        Amount = i.Amount,
                        Time = i.CreateDateTime
                    }).ToList()
                };

                var output = gameProcess.Payments.FirstOrDefault(p => p.Recipient != null && p.Recipient.Id == c.Key.Id);
                if (output != null)
                {
                    gameprocess.OutPut = new PlayerStuff
                    {
                        Amount = output.Amount,
                        Time = output.CreateDateTime
                    };
                }

                return gameprocess;
            }).ToList();

            return gameProcessItems;
        }
    }
}
 