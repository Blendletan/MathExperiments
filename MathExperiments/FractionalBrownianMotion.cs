using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public class FractionalBrownianMotion
    {
        private static double CorrelationFunction(double t, double s,double hurstExponent)
        {
            double exponent = 2 * hurstExponent;
            double output = AbsPow(t, exponent) + AbsPow(s, exponent) - AbsPow(t - s, exponent);
            output *= 0.5d;
            return output;
        }
        private static double AbsPow(double expBase, double expExponent)
        {
            return Math.Pow(Math.Abs(expBase), expExponent);
        }
        public static double[] Generate(int numberOfSteps, double hurstExponent)
        {
            var covariance = GenerateCovarianceMatrix(numberOfSteps, hurstExponent);
            var eign = covariance.Evd();
            var eigVec = eign.EigenVectors;
            var eigVecInverse = eigVec.Inverse();
            var diagonal = eign.D;
            for (int i = 0; i < numberOfSteps; i++)
            {
                double d = diagonal[i, i];
                d = Math.Sqrt(d);
                diagonal[i, i] = d;
            }
            var variationMatrix = eigVec * diagonal * eigVecInverse;
            var samples = Vector<double>.Build.Random(numberOfSteps);
            var output = variationMatrix * samples;
            return output.ToArray();
        }
        public static Matrix<double> GenerateCovarianceMatrix(int numberOfSteps, double hurstExponent)
        {
            var output = Matrix<double>.Build.Dense(numberOfSteps, numberOfSteps);
            double stepSize = 1d / (double)numberOfSteps;
            for (int i = 0; i < numberOfSteps; i++)
            {
                double t = stepSize * i;
                for (int j = i; j < numberOfSteps; j++)
                {
                    double s = stepSize * j;
                    double correlation = CorrelationFunction(t, s,hurstExponent);
                    output[i, j] = correlation;
                    output[j, i] = correlation;
                }
            }
            return output;
        }
    }
}
