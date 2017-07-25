using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulka.Models.Player
{
    public class PlayerVisitation
    {
        public PlayerVisitation()
        {
            VisitationItems = new List<PlayerVisitationItem>();
        }

        public List<PlayerVisitationItem> VisitationItems { get; set; }
    }

    public class PlayerVisitationItem
    {
        public string ClubName { get; set; }
        public string Persent { get; set; }
    }
}