using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public class PartitionsOfUnity
    {
        public static double DiracKernel(double x, double a)
        {
            double output = Eta(x / a);
            output /= TotalIntegral(a, 0.001);
            return output;
        }
        static double Eta(double x)
        {
            if (x >= 1 || x <= -1)
            {
                return 0;
            }
            double exponent = 1/(1 - x * x);
            double output = Math.Exp(-exponent);
            return output;
        }
        static double TotalIntegral(double max, double dx)
        {
            int numberOfSlices = (int)(2*max / dx);
            double output = 0;
            for (int i = 0; i < numberOfSlices; i++)
            {
                double x = i * dx-max;
                double y = Eta(x/max);
                output += dx * y;
            }
            return output;
        }
    }
}
