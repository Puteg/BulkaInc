using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bulka.Models.GameProcess
{
    public class TestGameProcessViewModel
    {
        public TestGameProcessViewModel()
        {
            ClubSelectListItems = new List<SelectListItem>();
        }

        [Display(Name = "Начать с")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Кол-во дней")]
        public int DayCount { get; set; }

        [Display(Name = "Клуб")]
        public int ClubId { get; set; }

        [Display(Name = "Кол-во игроков")]
        public int PlayersCount { get; set; }

        [Display(Name = "Минимальный вход")]
        public int MinAmount { get; set; }

        [Display(Name = "Максимальный вход")]
        public int MaxAmount { get; set; }

        [Display(Name = "Кол-во докупок")]
        public int AvgCountRebuy { get; set; }

        public List<SelectListItem> ClubSelectListItems { get; set; }
    }
}