using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcretoArmado
{
    class Loads
    {
        public double Gamac { get; set; }
        public double Gamas { get; set; }
        public double Gamaf { get; set; }
        public double Beta { get; set; }
        public double Amk { get; set; }

        public Loads()
        {
            Gamac = 1.4;
            Gamas = 1.15;
            Gamaf = 1.4;
            Beta = 1;

        }
    }
}
