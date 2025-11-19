namespace PrimeGaps
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string filePath = @"Poisson\test";
            int max = 1800000000;
            var p = PrimeGaps.GetPrimeGaps(max);
            int h = 100*(int)Math.Log(max);
            var newP = new double[h];
            for (int i = 0; i < h; i++)
            {
                newP[i] = p[p.Length - h + i];
            }
            p = newP;
            double total = (double)p.Length;
            int distinctGaps = (int)p.Max();
            double[] probabilities = new double[distinctGaps + 1];
            foreach (var v in p)
            {
                int gap = (int)v;
                probabilities[gap]++;
            }
            for (int i = 0; i <= distinctGaps; i++)
            {
                probabilities[i] /= total;
            }
            PlotHelper.DrawBar($"{filePath}_actual.bmp",probabilities);
            var randomProbabilities = GeneratePoisson(distinctGaps, max);
            PlotHelper.DrawBar($"{filePath}_random.bmp", randomProbabilities);
            var errorProbabilities = new double[distinctGaps + 1];
            for (int i = 0; i <= distinctGaps; i++)
            {
                errorProbabilities[i] = Math.Abs(probabilities[i] - randomProbabilities[i]);
            }
            PlotHelper.DrawBar($"{filePath}_error.bmp", errorProbabilities);
            //var g = DiscreteDifferences<double>.GetDifferences(p.ToList());
            //g = DiscreteDifferences<double>.GetDifferences(p.ToList());
            //PlotHelper.DrawHistogram(filePath, p.ToArray());
            Console.WriteLine("Hello, World!");
        }
        static double[] GeneratePoisson(int numberOfGaps,int location)
        {
            double p = 1/Math.Log((double)location);
            double[] probabilities = new double[numberOfGaps + 1];
            for (int i = 0; i <= numberOfGaps; i++)
            {
                double gap = (double)i;
                double prob = p * Math.Exp(-gap * p);
                probabilities[i] = prob;
            }
            return probabilities;
        }
    }
}
