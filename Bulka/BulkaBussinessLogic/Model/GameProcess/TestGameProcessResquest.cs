using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkaBussinessLogic.Model.GameProcess
{
    public class TestGameProcessResquest
    {
        public DateTime DateTime { get; set; }
        public int DayCount { get; set; }
        public int ClubId { get; set; }
        public int PlayersCount { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public int AvgCountRebuy { get; set; }
    }
}
