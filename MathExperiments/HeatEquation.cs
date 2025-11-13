using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public class HeatEquation
    {
        public static double[] Step(double[] previousValues, double timeInterval, double leftEndpoint, double rightEndpoint)
        {
            int length = previousValues.Length;
            var output = new double[length];
            output[0] = leftEndpoint;
            output[length - 1] = rightEndpoint;
            for (int i = 1; i < length - 1; i++)
            {
                double secondDerivative = previousValues[i + 1] - 2 * previousValues[i] + previousValues[i - 1];
                output[i] = previousValues[i] + timeInterval*secondDerivative;
            }
            return output;
        }
    }
}
