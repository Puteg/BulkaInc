using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Bulka.DataModel;

namespace Bulka.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BulkaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BulkaContext context)
        {
            IList<Player> players = new List<Player>();

            players.Add(new Player()
            {
                Id = 1,
                Name = "Алексей Гусев",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img1.jpg"
            });

            players.Add(new Player()
            {
                Id = 2,
                Name = "Ирина Киселёва",
                Account = new Account(),
                Address = "метро Беговая",
                Phone = "+ 7 966 333 22 12",
                AdditionInfo = "Любит кольян",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img2.jpg"
            });

            players.Add(new Player()
            {
                Id = 3,
                Name = "Вячеслав Воронцов",
                Account = new Account(),
                Address = "метро Текстильщиков",
                Phone = "+ 7 966 333 22 13",
                AdditionInfo = "Лудик",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img3.jpg"
            });

            players.Add(new Player()
            {
                Id = 4,
                Name = "Антон Погребняк",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 14",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img4.jpg"
            });

            players.Add(new Player()
            {
                Id = 5,
                Name = "Алекскей Смолкин",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img5.jpg"
            });

            players.Add(new Player()
            {
                Id = 6,
                Name = "Петя Соловьев",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img6.jpg"
            });

            players.Add(new Player()
            {
                Id = 7,
                Name = "Леван Акхалай",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img7.jpg"
            });

            players.Add(new Player()
            {
                Id = 8,
                Name = "Александр Дормачёв",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img8.jpg"
            });

            players.Add(new Player()
            {
                Id = 9,
                Name = "Андрей Козлов",
                Account = new Account(),
                Address = "метро Киевская",
                Phone = "+ 7 966 333 22 11",
                AdditionInfo = "Сильный игрок",
                Vk = "https://vk.com/alexsting1",
                ImageUrl = "/Images/img9.jpg"
            });

            foreach (Player std in players)
                context.Players.Add(std);

            base.Seed(context);
        }
    }
}
