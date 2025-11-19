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
        static public Complex[] GetFFT(double[] input)
        {
            int length = input.Length;
            Complex32[] values = new Complex32[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = (Complex32)input[i];
            }
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(values);
            Complex[] output = new Complex[length];
            for (int i = 0; i < length; i++)
            {
                output[i] = values[i].ToComplex();
            }
            return output;
        }
    }
}
