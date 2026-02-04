namespace SmoothPartitionsOfUnity
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            double max = 0.25;
            double dx = 0.01;
            int numberOfSteps = (int)(2 * max / dx);
            double[] xValues = new double[numberOfSteps];
            double[] yValues = new double[numberOfSteps];
            for (int i = 0; i < numberOfSteps; i++)
            {
                double x = i * dx - max;
                double y = PartitionsOfUnity.DiracKernel(x, max);
                xValues[i] = x;
                yValues[i] = y;
            }
            PlotHelper.DrawSignal("test2.bmp", xValues, yValues);
            Console.WriteLine("Hello, World!");
        }
    }
}
