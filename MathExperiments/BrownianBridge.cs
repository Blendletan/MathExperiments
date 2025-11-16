using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public class BrownianBridge
    {
        public List<double> FourierCoeffecients { get; }
        public BrownianBridge(int numberOfFourierTerms)
        {
            var iid = MathNet.Numerics.Generate.Normal(numberOfFourierTerms, 0, 1);
            for (int i = 0; i < numberOfFourierTerms; i++)
            {
                iid[i] *= Math.Sqrt(2);
                iid[i] /= Math.PI;
                iid[i] /= (double)(i+1);
            }
            FourierCoeffecients = iid.ToList();

        }
        public BrownianBridge(int numberOfFourierTerms, List<int> admissibleTerms,bool deterministic)
        {
            double[] coeff = new double[numberOfFourierTerms];
            if (deterministic == false)
            {
                coeff = MathNet.Numerics.Generate.Normal(numberOfFourierTerms, 0, 1);
            }
            for (int i = 0; i < numberOfFourierTerms; i++)
            {
                int index = i + 1;
                if (admissibleTerms.Contains(index) == false)
                {
                    coeff[i] = 0;
                    continue;
                }
                if (deterministic)
                {
                    coeff[i] = 1;
                }
                coeff[i] *= Math.Sqrt(2);
                coeff[i] /= Math.PI;
                coeff[i] /= (double)(index);
                if (deterministic)
                {
                    coeff[i] /= (double)(index);
                }
            }
            FourierCoeffecients = coeff.ToList();
        }
        public double Evaluate(double x)
        {
            double output = 0;
            int count = FourierCoeffecients.Count;
            for (int i = 1; i <= count; i++)
            {
                double angle = (double)i * x * Math.PI;
                output += FourierCoeffecients[i-1] * Math.Sin(angle);
            }
            return output;
        }
    }
}
