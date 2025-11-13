using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public class Goldbach
    {
        public static int[] Histogram(int maxHeight)
        {
            var p = Eratosthenes.GetPrimes(maxHeight).PrimeList;
            int count = p.Count;
            int pMax = p.Last();
            var output = new int[2 * pMax + 1];
            for (int i = 0; i < count; i++)
            {
                for (int j = i; j < count; j++)
                {
                    int sum = p[i] + p[j];
                    output[sum]++;
                }
            }
            return output;
        }
        public static void SaveHistogram(string filePath,int[] input)
        {
            int length = input.Length/2;
            var values = new double[length];
            var inputs = new double[length];
            for (int i = 4; i < length; i+=2)
            {
                inputs[i] = (double)i;
                values[i] = (double)input[i];
                double probability = 1 / Math.Log(inputs[i]);
                double numberOfPrimes = inputs[i] /(2 * Math.Log(inputs[i]/2));
                double expectedAnswer = probability * numberOfPrimes;
                values[i] /= expectedAnswer;
            }
            var plot = new ScottPlot.Plot();
            plot.Add.Bars(inputs,values);
            plot.Axes.Margins(bottom: 0.1);
            plot.Axes.Margins(left: 0.1);
            plot.Axes.AutoScale();
            plot.SaveBmp(filePath, 1000, 1000);
        }
    }
}
