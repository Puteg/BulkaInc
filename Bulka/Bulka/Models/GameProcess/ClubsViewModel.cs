using System;
using System.Collections.Generic;

namespace Bulka.Models.GameProcess
{
    public class ClubsViewModel
    {
        public ClubsViewModel()
        {
            Clubs = new List<ClubItemViewModel>();
        }

        public List<ClubItemViewModel> Clubs { get; set; }
    }

    public class ClubItemViewModel
    {
        public ClubItemViewModel()
        {
            Started = new List<GameProcessStartedViewModel>();
            Items = new List<GameProcessListItemViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayersCount { get; set; }
        public string Total { get; set; }

        public List<GameProcessStartedViewModel> Started { get; set; }
        public List<GameProcessListItemViewModel> Items { get; set; }
    }

    public class GameProcessStartedViewModel
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string DirationTime { get; set; }
    }

    public class GameProcessListItemViewModel
    {
        public int Id { get; set; }
        public string DateTime { get; set; }

        public int PlayerCount { get; set; }
        public string TotalInput { get; set; }
        public string TotalOutput { get; set; }
        public string Total { get; set; }
        public string DirationTime { get; set; }
    }
}