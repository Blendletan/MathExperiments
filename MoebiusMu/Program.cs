namespace MoebiusMu
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerateMuHistogram();
            var result = Statistics.GenerateChiSquareSamples(2, 1000000);
            PlotHelper.DrawHistogram("testChiSquareManySamples2.bmp", result);
        }
        static void GenerateMuHistogram()
        {
            int max = 1600000000;
            int sampleSize = 50;
            int numberOfTrials = 1000000;
            int maxStartingPoint = max - 2 * sampleSize;
            var moebius = Eratosthenes.MoebiusMu(max);
            var rng = new Random();
            var chiSquareHistogram = new double[numberOfTrials];
            for (int i = 0; i < numberOfTrials; i++)
            {
                int startingPoint = rng.Next(maxStartingPoint);
                List<long> muOutcomes = new List<long>();
                for (int j = 0; j < sampleSize; j++)
                {
                    muOutcomes.Add(moebius[startingPoint + j]);
                }
                double chi = ComputeChiSquare(muOutcomes);
                chiSquareHistogram[i] = chi;
            }
            PlotHelper.DrawHistogram("testSmallK4.bmp", chiSquareHistogram);
            /*
            for (int i = 1; i <= max; i++)
            {
                moebius[i] += moebius[i - 1];
            }
            var partialSums = new List<int>();
            var standardDeviations = new List<double>();
            var xValues = new List<double>();
            foreach (var v in moebius)
            {
                partialSums.Add(v);
                if (partialSums.Count < 3)
                {
                    continue;
                }
                var std = Statistics.StandardDeviation(partialSums);
                standardDeviations.Add(std);
                xValues.Add((double)partialSums.Count);
            }
            PlotHelper.DrawSignal("mertensStandardDeviations.bmp",xValues.ToArray(), standardDeviations.ToArray());
            for (int i = 0; i < xValues.Count; i++)
            {
                standardDeviations[i] = Math.Log(standardDeviations[i]);
                xValues[i] = Math.Log(xValues[i]);
            }
            PlotHelper.DrawSignal("mertensStandardDeviationsLogLog.bmp", xValues.ToArray(), standardDeviations.ToArray());
            */
            Console.WriteLine("Hello, World!");
        }
        static double ComputeChiSquare(List<long> outcomes)
        {
            int length = outcomes.Count;
            double expectedPlus = 3 / (Math.PI * Math.PI);
            double expectedZeros = 1 - 2 * expectedPlus;
            expectedPlus *= (double)length;
            double expectedMinus = expectedPlus;
            expectedZeros *= (double)length;
            double positives = 0;
            double negatives = 0;
            double zeros = 0;
            foreach (var outcome in outcomes)
            {
                if (outcome < 0)
                {
                    negatives++;
                }
                if (outcome > 0)
                {
                    positives++;
                }
                if (outcome == 0)
                {
                    zeros++;
                }
            }
            double output = 0;
            output += ChiSquareRatio(positives, expectedPlus);
            output += ChiSquareRatio(negatives, expectedMinus);
            output += ChiSquareRatio(zeros, expectedZeros);
            return output;
        }
        static double ChiSquareRatio(double n, double e)
        {
            double output = (n - e) * (n - e);
            output /= e;
            return output;
        }
    }
}
