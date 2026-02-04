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
            //int numberOfBins = (int)Math.Round(values.Max(), MidpointRounding.ToPositiveInfinity);
            int numberOfBins = 10;
            var hist = ScottPlot.Statistics.Histogram.WithBinCount(numberOfBins, values);
            ScottPlot.Plot plot = new ();
            var histPlot = plot.Add.Histogram(hist);
            histPlot.BarWidthFraction = 0.5;
            plot.Axes.MarginsX(0);
            plot.Axes.MarginsY(0);
            plot.Axes.AutoScaleExpandX();
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
        public static void DrawSignal(string filePath, double[] xValues, double[] yValues)
        {
            var plot = new ScottPlot.Plot();
            plot.Add.SignalXY(xValues, yValues);
            plot.Axes.Margins(bottom: 0.1);
            plot.Axes.Margins(left: 0.1);
            //plot.Axes.AutoScale();
            plot.SaveBmp(filePath, 1000, 1000);
        }
    }
}
