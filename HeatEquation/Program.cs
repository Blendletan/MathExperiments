namespace HeatEquation
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfPoints = 100000;
            int numberOfSteps = 10000000;
            int numberOfFourierTerms = 5000;
            var p = Eratosthenes.GetPrimes(numberOfFourierTerms);
            var bridge = new BrownianBridge(numberOfFourierTerms,p.PrimeList,false);
            double incrementSize = 1d / (double)numberOfPoints;
            var values = new double[numberOfPoints];
            int midpoint = numberOfPoints / 2;
            int midWidth = numberOfPoints / 10;
            for (int i = 0; i < numberOfPoints; i++)
            {
                double x = i * incrementSize;
                values[i] = Math.Abs(bridge.Evaluate(x));
            }
            string filePath = @"test2\test";
            int snapshotFrequency = 500;
            string numberOfDigits = $"D{(numberOfSteps/snapshotFrequency).ToString().Length}";
            int snapshotNumber = 0;
            for (int i = 0; i < numberOfSteps; i++)
            {
                values = HeatEquation.Step(values, incrementSize, 0, 0);
                if (i % snapshotFrequency == 0)
                {
                    string savePath = $"{filePath}_{snapshotNumber.ToString(numberOfDigits)}.bmp";
                    PlotHelper.DrawSignal(savePath, values);
                    Console.WriteLine($"Finished step {i} of {numberOfSteps}");
                    snapshotNumber++;
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
