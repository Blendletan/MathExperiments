using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    using MathNet.Numerics;
    using System.Numerics;
    public static class FFT
    {
        static public (double[] frequencies, Complex[] values) ContinuousFourierTransform(double leftEndpoint, double dx, double[] yValues)
        {
            int numberOfPoints = yValues.Length;
            double maxFrequency = 1 / dx;
            double df = 2*maxFrequency / (double)numberOfPoints;
            var frequencies = new double[numberOfPoints];
            var outputValues = new Complex[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                double frequency = -maxFrequency + i * df;
                frequencies[i] = frequency;
                outputValues[i] = FourierIntegral(leftEndpoint, dx, yValues, frequency);
            }
            return (frequencies, outputValues);
        }
        static Complex FourierIntegral(double leftEndpoint, double dx, double[] yValues,double frequency)
        {
            int numberOfPoints = yValues.Length;
            Complex output = 0;
            for (int i = 0; i < numberOfPoints; i++)
            {
                double x = leftEndpoint + (double)i * dx;
                Complex exponentialTerm = Complex.Exp(-Complex.ImaginaryOne * Math.Tau * x * frequency);
                Complex integrand = exponentialTerm * yValues[i];
                output += integrand * dx;
            }
            return output;
        }
        static public Complex[] GetFFT(double[] input)
        {
            int length = input.Length;
            Complex32[] values = new Complex32[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = (Complex32)input[i];
            }
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(values, MathNet.Numerics.IntegralTransforms.FourierOptions.Matlab);
            Complex[] output = new Complex[length];
            for (int i = 0; i < length; i++)
            {
                output[i] = values[i].ToComplex();
            }
            return output;
        }
    }
}
