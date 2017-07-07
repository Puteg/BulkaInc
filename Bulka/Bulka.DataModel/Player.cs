using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulka.DataModel
{
    public class Player
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public string FirstName { get; set; }
        public string Phone { get; set; }

        public virtual Account Account { get; set; }
    }
}
