namespace MoebiusMu
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerateMuHistogram();
            var result = Statistics.GenerateChiSquareSamples(2, 500);
            PlotHelper.DrawHistogram("testChiSquare.bmp", result);
        }
        static void GenerateMuHistogram()
        {
            int max = 100000000;
            int sampleSize = 500000;
            int numberOfTrials = 500;
            int maxStartingPoint = max - 2 * sampleSize;
            var moebius = Eratosthenes.MoebiusMu(max);
            var rng = new Random();
            var chiSquareHistogram = new double[numberOfTrials];
            for (int i = 0; i < numberOfTrials; i++)
            {
                int startingPoint = rng.Next(maxStartingPoint);
                List<int> muOutcomes = new List<int>();
                for (int j = 0; j < sampleSize; j++)
                {
                    muOutcomes.Add(moebius[startingPoint + j]);
                }
                double chi = ComputeChiSquare(muOutcomes);
                chiSquareHistogram[i] = chi;
            }
            PlotHelper.DrawHistogram("test2.bmp", chiSquareHistogram);
            Console.WriteLine("Hello, World!");
        }
        static double ComputeChiSquare(List<int> outcomes)
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
