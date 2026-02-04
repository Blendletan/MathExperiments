namespace PrimDFT
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 1000;
            var primeList = Eratosthenes.GetPrimes(x).PrimeList;
            var xValues = new double[x];
            var chebyshev = new double[x];
            var chebyshevSum = new double[x];
            for (int i=0;i<x;i++)
            {
                if (primeList.Contains(i+1))
                {
                    chebyshev[i] = Math.Log(i+1);
                }
                xValues[i] = (double)(i + 1);
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    chebyshevSum[i] += chebyshev[j];
                }
                chebyshev[i] -= i+1;
                chebyshev[i] /= Math.Sqrt(i+1);
            }
            PlotHelper.DrawSignal("scaledCheby.bmp", xValues, chebyshev);
            var fft = FFT.GetFFT(chebyshev).Select(x=>x.Magnitude).ToArray();
            var freqValues = new double[x];
            for (int i = 0; i < fft.Length; i++)
            {
                freqValues[i] = i;
                //freqValues[i] /= (double)x;
            }
            PlotHelper.DrawSignal("scaledChebyFFT.bmp", freqValues, fft);
            Console.WriteLine("Hello, World!");
        }
    }
}
