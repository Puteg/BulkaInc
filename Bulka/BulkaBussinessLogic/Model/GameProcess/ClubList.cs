using System;
using System.Collections.Generic;

namespace BulkaBussinessLogic.Model.GameProcess
{
    public class ClubList
    {
        public ClubList()
        {
            Clubs = new List<ClubItem>();
        }

        public List<ClubItem> Clubs { get; set; }
    }

    public class ClubItem
    {
        public ClubItem()
        {
            Items = new List<GameProcessListItem>();
        }

        public int Id { get;set; }
        public string Name { get;set; }
        public int PlayersCount { get; set; }
        public decimal Total { get; set; }

        public List<GameProcessListItem> Items { get; set; }
    }

    public class GameProcessListItem
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int PlayerCount { get; set; }
        public decimal TotalInput { get; set; }
        public decimal TotalOutput { get; set; }
        public decimal Total { get; set; }
        public TimeSpan DirationTime { get; set; }

        public bool IsFinish { get; set; }
    }
}
