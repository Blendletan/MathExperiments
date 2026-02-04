namespace UnitBall
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfSamples = 1000;
            for (int dimension = 1; dimension < 20; dimension++)
            {
                var output = GenerateSamples(numberOfSamples, dimension);
                PlotHelper.DrawHistogram($"Test_b__{dimension}.bmp", output);
                Console.WriteLine($"Finished dimension {dimension}");
            }
            Console.WriteLine("Hello, World!");
        }
        static double[] GenerateSamples(int numberOfSamples, int dimension)
        {
            var output = new double[numberOfSamples];
            for (int i = 0; i < numberOfSamples; i++)
            {
                output[i] = GeneratePointOnBall(dimension);
            }
            return output;
        }
        static double GeneratePointOnBall(int dimension)
        {
            var rng = new Random();
            while (true)
            {
                var points = new List<double>();
                for(int i = 0; i < dimension; i++)
                {
                    var nextPoint = 2*rng.NextDouble()-1;
                    points.Add(nextPoint);
                }
                var r = Radius(points);
                if (r < 1)
                {
                    return r;
                }
            }
        }
        static double Radius(List<double> points)
        {
            double output = 0;
            foreach (var v in points)
            {
                output += v * v;
            }
            return Math.Sqrt(output);
        }
    }
}
