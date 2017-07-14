using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;
using BulkaBussinessLogic.Implementation;
using NUnit.Framework;

namespace Bulka.Tests
{
    [TestFixture]
    class GameProcessTest
    {
        [Test]
        public void ProcessTest()
        {
            var club = new Club()
            {
                Name = "Булка 1"
            };
            var clubRepository = new ClubRepository();
            clubRepository.Add(club);
            clubRepository.Save();
           
            var player1 = new Player()
            {
                Account = new Account(),
                Name = "player1",
            };
            var player2 = new Player()
            {
                Account = new Account(),
                Name = "player2",
            };
            var player3 = new Player()
            {
                Account = new Account(),
                Name = "player3",
            };
            var player4 = new Player()
            {
                Account = new Account(),
                Name = "player4",
            };

            var playersRepository = new PlayersRepository();
            playersRepository.Add(player1);
            playersRepository.Add(player2);
            playersRepository.Add(player3);
            playersRepository.Add(player4);

            playersRepository.Save();

            var contex = new BulkaContext();
            var service = new GameProcessService(contex, club);
            
            service.StartProcess();

            service.Seat(player1, 10000);
            service.Seat(player2, 10000);
            service.Seat(player3, 10000);
            service.Seat(player4, 10000);

            service.Rebuy(player2, 20000);
            service.Rebuy(player2, 20000);
            service.Rebuy(player3, 20000);
            service.Rebuy(player4, 20000);

            service.SeatOut(player1, 50000);

            service.StopProcess();

            contex.SaveChanges();

            Assert.NotNull(service);
        }
    }
}
