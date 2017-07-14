using System.Collections.Generic;
using System.Web.Mvc;

namespace Bulka.Models
{
    public class GameProcessModel
    {
        public int Id { get; set; }

        public List<GameProcessItem> Items { get; set; }
        public List<SelectListItem> Players { get; set; }

        public ActionEditModel EditModel { get; set; }
    }

    public class GameProcessItem
    {
        public string PlayerName;
        public string PlayerImage;

        public List<PlayerStuff> Input { get; set; }
        public PlayerStuff OutPut { get; set; }
    }

    public class PlayerStuff
    {
        public string Amout { get; set; }
        public string Time { get; set; }
    }
}