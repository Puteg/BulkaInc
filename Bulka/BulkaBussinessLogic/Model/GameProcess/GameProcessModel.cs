using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BulkaBussinessLogic.Model.GameProcess
{
    public class GameProcessModel
    {
        public GameProcessModel()
        {
            Items = new List<GameProcessItem>();
            Players = new List<PlayerItem>();
        }

        public int Id { get; set; }

        public int PlayerCount { get; set; }
        public decimal TotalInput { get; set; }
        public decimal TotalOutput { get; set; }
        public decimal Total { get; set; }
        public TimeSpan DirationTime { get; set; }

        public List<GameProcessItem> Items { get; set; }
        public List<PlayerItem> Players { get; set; }

        public Action EditModel { get; set; }
    }

    public class PlayerItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
    }

    public class GameProcessItem
    {
        public GameProcessItem()
        {
            Input = new List<PlayerStuff>();
        }

        public int PlayerId;
        public string PlayerName;
        public string PlayerImage;

        public List<PlayerStuff> Input { get; set; }
        public PlayerStuff OutPut { get; set; }
    }

    public class PlayerStuff
    {
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
    }
}
