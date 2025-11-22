using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public static class Statistics
    {
        public static double[] GenerateChiSquareSamples(int df, int numberOfSamples)
        {
            var distribution = new MathNet.Numerics.Distributions.ChiSquared(df);
            var output = new double[numberOfSamples];
            distribution.Samples(output);
            return output;
        }
    }
}
