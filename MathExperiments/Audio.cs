using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MathExperiments
{
    public class AudioSample
    {
        readonly public List<double[]> Samples;
        readonly public int SampleRate;
        public AudioSample(List<double[]> samples, int sampleRate)
        {
            Samples = samples;
            SampleRate = sampleRate;
        }
    }
    public static class Audio
    {
        public static AudioSample ReadFile(string filePath)
        {
            var output = new List<double[]>();
            int sampleRate;
            using (var reader = new WaveFileReader(filePath))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                int numberOfChannels = reader.WaveFormat.Channels;
                int totalNumberOfBytes = (int)(reader.Length);
                int bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
                int numberOfSamples = totalNumberOfBytes / bytesPerSample;
                int numberOfFrames = numberOfSamples / numberOfChannels;
                for (int i = 0; i < numberOfChannels; i++)
                {
                    output.Add(new double[numberOfFrames]);
                }
                for (int i = 0; i < numberOfFrames; i++)
                {
                    var nextFrame = reader.ReadNextSampleFrame();
                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        output[j][i] = nextFrame[j];
                    }
                }
            }
            return new AudioSample(output, sampleRate);
        }
    }
}
