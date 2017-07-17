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
        public List<GameProcessListItem> Items { get; set; }
    }

    public class GameProcessListItem
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
