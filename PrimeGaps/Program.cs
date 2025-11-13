namespace PrimeGaps
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            //string filePath = @"thirdGap\test8.bmp";
            string filePath = "test9.bmp";
            var p = PrimeGaps.GetPrimeGaps(2000000000);
            //var g = DiscreteDifferences<double>.GetDifferences(p.ToList());
            //g = DiscreteDifferences<double>.GetDifferences(p.ToList());
            PlotHelper.DrawHistogram(filePath, p.ToArray());
            Console.WriteLine("Hello, World!");
        }
    }
}
