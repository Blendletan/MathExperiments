namespace FractalBrownianMotion
{
    using MathExperiments;
    using System.Diagnostics;
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"test";
            int numberOfSteps = 1000;
            var sw = new Stopwatch();
            for (int i = 1; i < 10; i++)
            {
                sw.Start();
                double hurst = (double)i / 10d;
                var output = FractionalBrownianMotion.Generate(numberOfSteps, hurst);
                string fileName = $"{filePath}_{i}.bmp";
                PlotHelper.DrawSignal(fileName, output);
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
                sw.Reset();
            }
            Console.WriteLine("Hello, World!");
        }
    }
}
