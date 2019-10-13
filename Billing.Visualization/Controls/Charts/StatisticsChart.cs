using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Billing.Service.Experiment;
using Billing.Service.Experiment.Models;

namespace Billing.Visualization.Controls.Charts
{
    public class StatisticsChart:Chart
    {
        private const string BinaryCopySeries = "Binary COPY";
        private const string CompositeTypesSeries = "Composite Types";
        private const string EntityFrameworkSeries = "Entity Framework";

        public StatisticsChart()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Location = new Point() {X = 10, Y = 10};
            Size = new Size(950, 570);
            ChartAreas.Add(new ChartArea());
            GenerateSeries();
            Legends.Add(EntityFrameworkSeries);
            Legends.Add(CompositeTypesSeries);
            Legends.Add(BinaryCopySeries);
        }

        public void AddPoint(MeasuredTime measuredTime)
        {
            var series = GetSeries(measuredTime);
            series.Points.AddXY(measuredTime.BatchSize, measuredTime.Average);
        }

        public void ResetSeries()
        {
            Series.Clear();
            GenerateSeries();
        }

        private void GenerateSeries()
        {
            foreach (var series in CreateSeries())
                Series.Add(series);
        }

        private Series[] CreateSeries()
        {
            return new []
            {
                new Series()
                {
                    Name = EntityFrameworkSeries,
                    ChartType = SeriesChartType.FastLine,
                    BorderWidth = 3,
                    Color = Color.Red
                },
                new Series()
                {
                    Name = CompositeTypesSeries,
                    ChartType = SeriesChartType.FastLine,
                    BorderWidth = 3,
                    Color = Color.Orange
                },
                new Series()
                {
                    Name = BinaryCopySeries,
                    ChartType = SeriesChartType.FastLine,
                    BorderWidth = 3, Color = Color.Blue
                }
            };
        }

        private Series GetSeries(MeasuredTime measuredTime)
        {
            return Series.FindByName(GetSeriesName(measuredTime.InsertionType));
        }

        private string GetSeriesName(InsertionType insertionType)
        {
            switch (insertionType)
            {
                case InsertionType.EntityFramework:
                    return EntityFrameworkSeries;
                case InsertionType.CompositeTypes:
                    return CompositeTypesSeries;
                case InsertionType.BinaryCopy:
                    return BinaryCopySeries;
                default:
                    throw new ArgumentOutOfRangeException(nameof(insertionType), insertionType, null);
            }
        }
    }
}