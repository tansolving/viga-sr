using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcretoArmado
{
    class SquareBeam
    {
        public double H { get; set; }
        public double B { get; set; }
        public double D1 { get; set; }
        public double D { get; set; }
        public double cobr { get; set; }
        public double L { get; set; }
        public SquareBeam(double h, double b)
        {
            H = h;
            B = b;
        }

        public SquareBeam()
        {
        }
    }

    class Concrete : SquareBeam
    {
        public double Fck { get; set; }
        public double Ec { get; set; }
    }

    class Steel : SquareBeam
    {
        public double Asl { get; set; }
        public double As { get; set; }
        public double Fyk { get; set; }
        public double Es { get; set; }
        public int Ncamadas { get; set; }
    }

}
