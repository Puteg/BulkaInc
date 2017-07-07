using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulka.DataModel
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<GameProcess> GameProcesses { get; set; } 
    }
}
