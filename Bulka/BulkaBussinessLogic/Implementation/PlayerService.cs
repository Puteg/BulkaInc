using System.Collections.Generic;
using System.Linq;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.DataModel.Enum;
using Bulka.Repository;

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

        public List<PlayerSession> GetSessions(int playerId)
        {
            var playerSessions = _playerSessionRepository.GetAll().Where(c => c.PlayerId == playerId).OrderByDescending(c => c.Begin).ToList();
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
    }
}
