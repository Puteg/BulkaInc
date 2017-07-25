using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulka.Models.Player
{
    public class PlayerSessionListViewModel
    {
        public PlayerSessionListViewModel()
        {
            Items = new List<PlayerSessionItemViewModel>();
        }

        public string Title { get; set; }

        public int DayCount { get; set; }
        public string AvgDuration { get; set; }
        public string AvgInput { get; set; }
        public string AvgOutput { get; set; }
        public string Profit { get; set; }

        public List<PlayerSessionItemViewModel> Items { get; set; }
    }
}