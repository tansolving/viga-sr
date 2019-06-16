using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConcretoArmado
{
    class Materials
    {
        public double Fck { get; set; }
        public double Fcd { get; set; }
        public double Fyk { get; set; }
        public double Fyd { get; set; }
        public double Es { get; set; }

        public Materials()
        {
            Fck = 20;
            Fyk = 500;
            Es = 200000;
        }

        public double MpaToKncm2(double value)
        {
            return value / 10;
        }

        public double FckToFcd(double value, double gammac)
        {
            return value / gammac;
        }

    }
}
