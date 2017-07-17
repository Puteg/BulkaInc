﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace BulkaBussinessLogic.Model.GameProcess
{
    public class GameProcessModel
    {
        public GameProcessModel()
        {
            Items = new List<GameProcessItem>();
            Players = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public int PlayerCount { get; set; }
        public string TotalInput { get; set; }
        public string TotalOutput { get; set; }
        public string Total { get; set; }
        public string DirationTime { get; set; }

        public List<GameProcessItem> Items { get; set; }
        public List<SelectListItem> Players { get; set; }

        public ActionEditModel EditModel { get; set; }
    }

    public class GameProcessItem
    {
        public GameProcessItem()
        {
            Input = new List<PlayerStuff>();
        }

        public string PlayerName;
        public string PlayerImage;

        public List<PlayerStuff> Input { get; set; }
        public PlayerStuff OutPut { get; set; }
    }

    public class PlayerStuff
    {
        public decimal Amount { get; set; }
        public string Time { get; set; }
    }
}