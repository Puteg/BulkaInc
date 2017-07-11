using System;

namespace Bulka.DataModel
{
    public class PlayerSession
    {
        public long Id { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public decimal Input { get; set; }
        public decimal Output { get; set; }

        public virtual Player Player { get; set; }
        public virtual Club Club { get; set; }
    }
}
