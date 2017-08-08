using System;
using Bulka.DataAccess;
using BulkaBussinessLogic.Implementation;
using BulkaBussinessLogic.Model.GameProcess;
using NUnit.Framework;

namespace Bulka.Tests
{
    [TestFixture]
    class GameProcessTest
    {
        [Test]
        public void StringTest()
        {
            long numb = 7966333221;
            var str = @String.Format("{0:+# (###) ###-##-##}", numb);

            Assert.NotNull(str);
        }

        [Test]
        public void ProcessTest()
        {
            var gameProcess = new GameProcessService(new BulkaContext());

            var request = new TestGameProcessResquest()
            {
                DateTime = DateTime.Now,
                ClubId = 12,
                PlayersCount = 9,
                MaxAmount = 10000,
                MinAmount = 3000,
                AvgCountRebuy = 4
            };


            for (int i = 0; i < 10; i++)
            {
                request.DateTime = request.DateTime.AddDays(1);

                gameProcess.CreateTestGameProcess(request);
            }
        }
    }
}
