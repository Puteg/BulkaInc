using System.Collections.Generic;

namespace Bulka.Models.Player
{
    public class PlayerProfileViewModel
    {
        public PlayerProfileViewModel()
        {
            Sessions = new List<PlayerSessionListViewModel>();
            Visitation = new PlayerVisitation();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Vk { get; set; }
        public string ImageUrl { get; set; }
        public string AdditionInfo { get; set; }

        public List<PlayerSessionListViewModel> Sessions { get; set; }
        public PlayerVisitation Visitation { get; set; }
    }
}