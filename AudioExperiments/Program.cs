namespace AudioExperiments
{
    using MathExperiments;
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "test.wav";
            var audio = Audio.ReadFile(filePath);
            Console.WriteLine($"There are {audio.Samples.Count} channels, using the 0th channel");
            double sampleRate = (double)audio.SampleRate;
            double timeIncrement = 1 / sampleRate;
            var yValues = audio.Samples[0];
            int length = yValues.Length;
            var xValues = new double[length];
            for (int i = 0; i < length; i++)
            {
                xValues[i] = timeIncrement;
                xValues[i] *= (double)i;
            }
            PlotHelper.DrawSignal("signal.bmp", xValues, yValues);
            var fourier = FFT.GetFFT(yValues);
            var amplitudes = fourier.Select(x => x.Magnitude).ToArray();
            var realFourier = fourier.Select(x => x.Real).ToArray();
            var imaginaryFourier = fourier.Select(x => x.Imaginary).ToArray();
            var amplitudesTrimmed = new double[length / 2];
            var frequencyValues = new double[length / 2];
            var fullFrequencyValues = new double[length];
            for (int i = 0; i < length; i++)
            {
                fullFrequencyValues[i] = sampleRate;
                fullFrequencyValues[i] *= (double)i;
                fullFrequencyValues[i] /= (double)length;
            }
            for (int i = 0; i < length/2; i++)
            {
                amplitudesTrimmed[i] = amplitudes[i];
                frequencyValues[i] = fullFrequencyValues[i];
            }
            PlotHelper.DrawSignal("FFT.bmp", frequencyValues, amplitudesTrimmed);
            PlotHelper.DrawSignal("FFT_REAL.bmp", fullFrequencyValues, realFourier);
            PlotHelper.DrawSignal("FFT_IMAGINARY.bmp", fullFrequencyValues, imaginaryFourier);
            var fourierPeaks = amplitudesTrimmed.OrderByDescending(x => x).Take(5);
            for (int i = 0; i < length/2; i++)
            {
                var currentAmplitude = amplitudes[i];
                if (fourierPeaks.Contains(currentAmplitude))
                {
                    Console.WriteLine($"Fourier series peaking at {frequencyValues[i]} Hz with amplitude {currentAmplitude}");
                }
            }
            Console.WriteLine("Hello, World!");
        }
    }
}
