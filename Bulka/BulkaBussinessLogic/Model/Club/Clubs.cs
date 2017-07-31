using System.Collections.Generic;

namespace BulkaBussinessLogic.Model.Club
{
    public class Clubs
    {
        public Clubs()
        {
            Items = new List<ClubItem>();
        }

        public List<ClubItem> Items { get; set; }
    }
}
