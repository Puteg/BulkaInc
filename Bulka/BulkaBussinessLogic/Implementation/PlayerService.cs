using System;
using System.Collections.Generic;
using System.Linq;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.DataModel.Enum;
using Bulka.Repository;
using BulkaBussinessLogic.Model.GameProcess;

namespace BulkaBussinessLogic.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayersRepository _playersRepository;
        private readonly PlayerSessionRepository _playerSessionRepository;

        public PlayerService(BulkaContext context)
        {
            _playersRepository = new PlayersRepository(context);
            _playerSessionRepository = new PlayerSessionRepository(context);
        }

        public List<PlayerItem> Search(string query)
        {
            var players = _playersRepository.GetAll().Where(c => c.Name.Contains(query) || c.Phone == query).ToList();
            return players.Select(c =>
            {
                var phone = (c.Phone ?? "0").Replace("+", "").Replace(" ", "");
                return new PlayerItem
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Phone = long.Parse(phone),
                    ImageUrl = c.ImageUrl,
                };
            }).ToList();
        }

        public List<PlayerSession> GetSessions(int playerId)
        {
            var playerSessions = _playerSessionRepository.GetAll().Where(c => c.PlayerId == playerId).ToList();
            return playerSessions;
        }

        public Player Create(string firstName, string phone)
        {
            var newAccount = new Account()
            {
                Type = AccountType.Virtual
            };

            var newPlayer = new Player()
            {
                Name = firstName,
                Phone = phone,
                Account = newAccount
            };

            _playersRepository.Add(newPlayer);
            _playersRepository.Save();

            return newPlayer;
        }

        public List<PlayerItem> GetAll()
        {
            var players = _playersRepository.GetAll().ToList();
            var playerSessions = _playerSessionRepository.GetAll().ToList();

            var playersList = players.Select(c =>
            {
                PlayerItem player;
                var sessions = playerSessions.Where(p => p.PlayerId == c.Id).ToList();
                var phone = (c.Phone ?? "0").Replace("+", "").Replace(" ", "");

                if (sessions.Any())
                {
                    var last = sessions.OrderBy(s => s.End).Take(1).First();
                    player = new PlayerItem
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Phone = long.Parse(phone),
                        Vk = c.Vk,
                        Address = c.Address,
                        AdditionInfo = c.AdditionInfo,
                        ImageUrl = c.ImageUrl,

                        GameCount = sessions.Count(),
                        Input = sessions.Sum(s => s.Input),
                        Output = sessions.Sum(s => s.Output),
                        Time = new TimeSpan(sessions.Sum(s => s.End.Subtract(s.Begin).Ticks)/sessions.Count()),
                        Total = sessions.Sum(s => s.Output - s.Input),
                        LastGameDate = last.End,
                        LastGameClub = last.Club.Name
                    };
                }
                else
                {
                    player = new PlayerItem
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Phone = long.Parse(phone),
                        Vk = c.Vk,
                        Address = c.Address,
                        AdditionInfo = c.AdditionInfo,
                        ImageUrl = c.ImageUrl,
                    };
                }

                return player;
            }).ToList();

            return playersList;
        }
    }
}
