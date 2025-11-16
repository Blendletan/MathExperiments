using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MathExperiments
{
    public class PlotHelper
    {
        public static void DrawBar(string filePath, double[] values)
        {
            var plot = new ScottPlot.Plot();
            plot.Add.Bars(values);
            plot.Axes.Margins(bottom: 0.1);
            plot.Axes.Margins(left: 0.1);
            plot.Axes.AutoScale();
            plot.SaveBmp(filePath, 1000, 1000);
        }
        public static void DrawHistogram(string filePath, double[] values)
        {
            int numberOfBins = (int)Math.Round(values.Max(), MidpointRounding.ToPositiveInfinity);
            var hist = ScottPlot.Statistics.Histogram.WithBinCount(numberOfBins, values);
            var plot = new ScottPlot.Plot();
            plot.Add.Bars(hist.Bins, hist.Counts);
            plot.Axes.Margins(bottom: 0.1);
            plot.Axes.Margins(left: 0.1);
            plot.Axes.AutoScale();
            plot.SaveBmp(filePath, 1000, 1000);
        }
        public static void DrawSignal(string filePath, double[] values)
        {
            var plot = new ScottPlot.Plot();
            plot.Add.Signal(values);
            plot.Axes.Margins(bottom: 0.1);
            plot.Axes.Margins(left: 0.1);
            //plot.Axes.AutoScale();
            plot.SaveBmp(filePath, 1000, 1000);
        }
    }
}
