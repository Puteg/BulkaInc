using System.ComponentModel.DataAnnotations;

namespace Bulka.Models
{
    public class PlayerEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Фото")]
        public string ImageUrl { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Вконтакте")]
        public string Vk { get; set; }
        
        [Display(Name = "Информация")]
        public string AdditionInfo { get; set; }
    }
}