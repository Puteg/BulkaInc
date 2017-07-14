using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulka.Models
{
    public class ActionEditModel
    {
        public int GameProcessId { get; set; }
        public int PlayerId { get; set; }
        public decimal Amount { get; set; }

        public ActionType Type { get; set; }
    }

    public enum ActionType
    {
        Seat = 0,
        Rebuy = 1,
        SeatOut = 2
    }
}