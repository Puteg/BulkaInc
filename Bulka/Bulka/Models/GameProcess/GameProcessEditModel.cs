using System.Collections.Generic;
using System.Web.Mvc;

namespace Bulka.Models.GameProcess
{
    public class GameProcessEditModel
    {
        public GameProcessEditModel()
        {
            Items = new List<GameProcessItemViewModel>();
            Players = new List<SelectListItem>();
            EditModel = new ActionEditModel();
        }

        public int Id { get; set; }
        public int PlayerCount { get; set; }
        public string TotalInput { get; set; }
        public string TotalOutput { get; set; }
        public string Total { get; set; }
        public string DirationTime { get; set; }

        public List<GameProcessItemViewModel> Items { get; set; }
        public List<SelectListItem> Players { get; set; }

        public ActionEditModel EditModel { get; set; }
    }
}