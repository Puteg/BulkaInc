using System;

namespace Bulka.DataModel
{
    public class Player
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Vk { get; set; }
        public string ImageUrl { get; set; }
        public string AdditionInfo { get; set; }

        public virtual Account Account { get; set; }
    }
}
