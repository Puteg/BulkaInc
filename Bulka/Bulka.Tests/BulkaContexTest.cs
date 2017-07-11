using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;
using NUnit.Framework;

namespace Bulka.Tests
{
    [TestFixture]
    class BulkaContexTest
    {
        [Test]
        public void PlayersAll()
        {
            var playersRepository = new PlayersRepository();
            var players = playersRepository.GetAll().ToList();
            
            Assert.NotNull(players);
        }

        [Test]
        public void Players()
        {
            var db = new BulkaContext();

            db.Players.Add(new Player()
            {
                Id = 1,
                Name = "Egor",
                Phone = "666"
            });
            db.SaveChanges();

            var players = db.Players.ToList();


            Assert.NotNull(players);
        }
    }
}
