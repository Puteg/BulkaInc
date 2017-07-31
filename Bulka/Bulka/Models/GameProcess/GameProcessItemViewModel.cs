using System.Collections.Generic;

namespace Bulka.Models.GameProcess
{
    public class GameProcessItemViewModel
    {
        public GameProcessItemViewModel()
        {
            Input = new List<PlayerStuffViewModel>();
        }

        public int PlayerId;
        public string PlayerName;
        public string PlayerImage;

        public List<PlayerStuffViewModel> Input { get; set; }
        public PlayerStuffViewModel OutPut { get; set; }
    }
}