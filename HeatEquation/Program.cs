namespace HeatEquation
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            FFTExperiment(440, 50, 1000000, @"FFT\test");
        }
        static void FFTExperiment(int numberOfSamples, int numberOfRepititions, int numberOfSteps, string filePath)
        {
            double epsilon=0.02;
            int samplesLength = numberOfSamples * numberOfRepititions;
            double[] samples = new double[samplesLength];
            double incrementSize = 1d / (double)numberOfSamples;
            incrementSize *= Math.Tau;
            var bridge = new BrownianBridge(100);
            var noise = new double[samplesLength];
            for (int i = 0; i < samplesLength; i++)
            {
                double xValue = (double)i;
                xValue *= incrementSize;
                noise[i] = bridge.Evaluate(xValue);
            }
            for (int i = 0; i < numberOfSamples; i++)
            {
                double xValue = (double)i;
                xValue *= incrementSize;
                for (int j = 0; j < numberOfRepititions; j++)
                {
                    int index = i + numberOfSamples * j;
                    samples[index] = Math.Sin(1.1 * xValue);
                    samples[index] += epsilon *noise[index];
                }
            }
            PlotHelper.DrawSignal($"{filePath}_ORIGINAL.bmp", samples);
            var fourier = FFT.GetFFT(samples);
            var realPart = new double[samplesLength];
            var imaginaryPart = new double[samplesLength];
            var amplitude = new double[samplesLength];
            for (int i = 0; i < samplesLength; i++)
            {
                realPart[i] = fourier[i].Real;
                imaginaryPart[i] = fourier[i].Imaginary;
                amplitude[i] = fourier[i].Magnitude;
            }
            PlotHelper.DrawSignal($"{filePath}_REAL.bmp", realPart);
            PlotHelper.DrawSignal($"{filePath}_IMAGINAY.bmp", imaginaryPart);
            PlotHelper.DrawSignal($"{filePath}_AMPLITUDE.bmp", amplitude);
            for (int i = 0; i < numberOfSteps; i++)
            {
                samples = HeatEquation.Step(samples, incrementSize, 0, 0);
            }
            PlotHelper.DrawSignal($"{filePath}_FINAL.bmp", samples);
            fourier = FFT.GetFFT(samples);
            realPart = new double[samplesLength];
            imaginaryPart = new double[samplesLength];
            amplitude = new double[samplesLength];
            for (int i = 0; i < samplesLength; i++)
            {
                realPart[i] = fourier[i].Real;
                imaginaryPart[i] = fourier[i].Imaginary;
                amplitude[i] = fourier[i].Magnitude;
            }
            PlotHelper.DrawSignal($"{filePath}_FINAL_REAL.bmp", realPart);
            PlotHelper.DrawSignal($"{filePath}_FINAL_IMAGINAY.bmp", imaginaryPart);
            PlotHelper.DrawSignal($"{filePath}_FINAL_AMPLITUDE.bmp", amplitude);
        }
        static void ExperimentOne()
        {
            int numberOfPoints = 100000;
            int numberOfSteps = 10000000;
            int numberOfFourierTerms = 5000;
            var p = Eratosthenes.GetPrimes(numberOfFourierTerms);
            var bridge = new BrownianBridge(numberOfFourierTerms, p.PrimeList, false);
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
            string numberOfDigits = $"D{(numberOfSteps / snapshotFrequency).ToString().Length}";
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
