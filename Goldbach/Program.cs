namespace Goldbach
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"goldbach14.bmp";
            int maxHeight = 5000000;
            var h = Goldbach.Histogram(maxHeight);
            Goldbach.SaveHistogram(filePath, h);
            Console.WriteLine("Hello, World!");
        }
    }
}
