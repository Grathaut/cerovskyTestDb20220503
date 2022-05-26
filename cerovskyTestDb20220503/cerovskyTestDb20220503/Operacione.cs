using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerovskyTestDb20220503
{
    public class Operacione
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int PriceForOnePiece { get; set; }
        public int DPH { get; set; }

        public Bill Bill { get; set; } //nevím proč, ale Bill nefunguje
    }
}
