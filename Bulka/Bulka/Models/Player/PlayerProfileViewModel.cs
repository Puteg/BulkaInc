using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulka.Models.Player
{
    public class PlayerProfileViewModel
    {
        public PlayerProfileViewModel()
        {
            PlayerSessionList = new List<PlayerSessionListViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Vk { get; set; }
        public string ImageUrl { get; set; }
        public string AdditionInfo { get; set; }

        public List<PlayerSessionListViewModel> PlayerSessionList { get; set; }
        public List<IGrouping<string, PlayerSessionListViewModel>> Grouping { get; set; }
    }
}