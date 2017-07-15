using System;
using System.Collections.Generic;

namespace BulkaBussinessLogic.Model.GameProcess
{
    public class GameProcessList
    {
        public GameProcessList()
        {
            Items = new List<GameProcessListItem>();
        }

        public List<GameProcessListItem> Items { get; set; }
    }

    public class GameProcessListItem
    {
        public int Id { get; set; }
        public string DateTime { get; set; }

        public int PlayerCount { get; set; }
        public decimal TotalInput { get; set; }
        public decimal TotalOutput { get; set; }
        public decimal Total { get; set; }
        public string DirationTime { get; set; }
    }
}
