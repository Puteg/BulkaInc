using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkaBussinessLogic.Model.Club
{
    public class ClubList
    {
        public ClubList()
        {
            Clubs = new List<ClubListItem>();
        }

        public List<ClubListItem> Clubs { get; set; }
    }
}
