using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcretoArmado
{
    class Geometry : SquareBeam
    {
        public double D { get; set; }
        public double Dl { get; set; }

        public Geometry()
        {
            D = 0.9 * H;
            Dl = 4.0;
        }
    }
}
