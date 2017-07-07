using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulka.DataModel
{
    public class GameProcess
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Guid AccountId { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual Club Club { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}
