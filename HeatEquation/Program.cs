namespace HeatEquation
{
    using MathExperiments;
    using System.Numerics;
    internal class Program
    {
        static void Main(string[] args)
        {
            HeatOnLine2(@"RealLine\heatTest",100000, 7, 500);
        }
        static void HeatOnLine2(string filePath, int numberOfIterations, int maxRealValue, int numberOfPoints)
        {
            double leftEndpoint = -(double)maxRealValue;
            double incrementSize = (double)maxRealValue;
            incrementSize *= 2.0;
            incrementSize /= (double)numberOfPoints;
            double[] xValues = new double[numberOfPoints];
            double[] yValues = new double[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                double x = leftEndpoint + incrementSize * (double)i;
                xValues[i] = x;
                double y = Math.Exp(-Math.Abs(x));
                yValues[i] = y;
            }
            PlotHelper.DrawSignal($"{filePath}_Signal.bmp", xValues, yValues);
            var fourier = FFT.ContinuousFourierTransform(leftEndpoint, incrementSize, yValues);
            PlotHelper.DrawSignal($"{filePath}_transform.bmp", fourier.frequencies, fourier.values.Select(x=>x.Magnitude).ToArray());
            for (int i = 0; i < numberOfIterations; i++)
            {
                yValues = HeatEquation.Step(yValues, incrementSize, 0, 0);
            }
            PlotHelper.DrawSignal($"{filePath}_Signal_Final.bmp", xValues, yValues);
            fourier = FFT.ContinuousFourierTransform(leftEndpoint, incrementSize, yValues);
            PlotHelper.DrawSignal($"{filePath}_transform_Final.bmp", fourier.frequencies, fourier.values.Select(x => x.Magnitude).ToArray());
        }
        static void HeatOnLine(string filePath,int numberOfIterations, int maxRealValue, int numberOfPoints)
        {
            double leftEndpoint = -(double)maxRealValue;
            double incrementSize = (double)maxRealValue;
            incrementSize *= 2.0;
            incrementSize /= (double)numberOfPoints;
            double frequencyIncrement = 2*maxRealValue / (incrementSize);
            double[] xValues = new double[numberOfPoints];
            double[] yValues = new double[numberOfPoints];
            double[] frequencies = new double[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                double x = (double)i;
                x *= incrementSize;
                x -= (double)maxRealValue;
                xValues[i] = x;
                double y = Math.Exp(-x*x);
                yValues[i] = y;
                var freq = frequencyIncrement;
                if (i < numberOfPoints / 2)
                {
                    freq *= (double)i;
                }
                else
                {
                    freq *= (double)(numberOfPoints - i);
                }
                frequencies[i] = freq;
            }
            var fourier = FFT.GetFFT(yValues.Select(x=>x*incrementSize).ToArray());
            for (int i = 0; i < numberOfPoints; i++)
            {
                fourier[i] *= Complex.Exp(Complex.ImaginaryOne * Math.Tau * leftEndpoint);
                fourier[i] *= incrementSize;
            }
            double[] transform = fourier.Select(x => x.Magnitude).ToArray();
            PlotHelper.DrawSignal($"{filePath}original.bmp", xValues, yValues);
            PlotHelper.DrawSignal($"{filePath}fourier.bmp", frequencies, transform);
            for (int i = 0; i < numberOfIterations; i++)
            {
                yValues=HeatEquation.Step(yValues, incrementSize, 0, 0);
            }
            fourier = FFT.GetFFT(yValues.Select(x => x * incrementSize).ToArray());
            for (int i = 0; i < numberOfPoints; i++)
            {
                fourier[i] *= Complex.Exp(Complex.ImaginaryOne * Math.Tau * leftEndpoint);
                fourier[i] *= frequencyIncrement;
            }
            transform = fourier.Select(x => x.Magnitude).ToArray();
            PlotHelper.DrawSignal($"{filePath}_FINAL_singal.bmp", xValues, yValues);
            PlotHelper.DrawSignal($"{filePath}_FINAL_fourier.bmp", frequencies, transform);
        }
        static void FFTExperiment(int numberOfSamples, int numberOfRepititions, int numberOfSteps, string filePath)
        {
            double epsilon = 0.02;
            int samplesLength = numberOfSamples * numberOfRepititions;
            double[] samples = new double[samplesLength];
            double[] xValues = new double[samplesLength];
            double[] frequencies = new double[samplesLength];
            double incrementSize = 1d / (double)numberOfSamples;
            incrementSize *= Math.Tau;
            var bridge = new BrownianBridge(100);
            var noise = new double[samplesLength];
            for (int i = 0; i < samplesLength; i++)
            {
                double xValue = (double)i;
                double frequency = xValue;
                xValue *= incrementSize;
                frequency /= incrementSize;
                frequency *= (double)samplesLength;
                xValues[i] = xValue;
                frequencies[i] = frequency;
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
                    samples[index] += epsilon * noise[index];
                }
            }
            PlotHelper.DrawSignal($"{filePath}_ORIGINAL.bmp", xValues, samples);
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
            PlotHelper.DrawSignal($"{filePath}_REAL.bmp", frequencies, realPart);
            PlotHelper.DrawSignal($"{filePath}_IMAGINAY.bmp", frequencies, imaginaryPart);
            PlotHelper.DrawSignal($"{filePath}_AMPLITUDE.bmp", frequencies, amplitude);
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
            PlotHelper.DrawSignal($"{filePath}_FINAL_REAL.bmp", frequencies, realPart);
            PlotHelper.DrawSignal($"{filePath}_FINAL_IMAGINAY.bmp", frequencies, imaginaryPart);
            PlotHelper.DrawSignal($"{filePath}_FINAL_AMPLITUDE.bmp", frequencies, amplitude);
            var max = amplitude.Max();
            for (int i = 0; i < samplesLength; i++)
            {
                if (amplitude[i] == max)
                {
                    var frequency = frequencies[i];
                    Console.WriteLine(frequency);
                    return;
                }
            }
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
