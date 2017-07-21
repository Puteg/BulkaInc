using System.Collections.Generic;
using System.Linq;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;

namespace BulkaBussinessLogic.Implementation
{
    public class PlayerSessionService
    {
        private readonly PlayerSessionRepository _playerSessionRepository;

        public PlayerSessionService(BulkaContext context)
        {
            _playerSessionRepository = new PlayerSessionRepository(context);
        }

        public List<PlayerSession> GetList(int playerId)
        {
            var playerSessionsDb = _playerSessionRepository.GetAll().Where(c => c.PlayerId == playerId).ToList();
            return playerSessionsDb;
        }

        public bool Create(List<PlayerSession> sessions)
        {
            foreach (var playerSession in sessions)
            {
                _playerSessionRepository.Add(playerSession);
            }
            
            _playerSessionRepository.Save();
            return true;
        }

    }
}
