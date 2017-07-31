using System.Collections.Generic;

namespace Bulka.Models.Club
{
    public class ClubsViewModel
    {
        public ClubsViewModel()
        {
            Items = new List<ClubListItemViewModel>();
        }

        public List<ClubListItemViewModel> Items { get; set; }
    }
}