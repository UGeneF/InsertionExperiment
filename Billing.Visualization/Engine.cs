using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Billing.Service.Experiment;
using Billing.Service.Experiment.Models;
using Billing.Visualization.Controls;

namespace Billing.Visualization
{
    public class Engine
    {
        private readonly Contols _contols;

        public Engine()
        {
            _contols = new Contols();
        }

        public Control[] GetLinkedControls()
        {
            LinkControls();
            return _contols.GetControls();
        }

        private void LinkControls()
        {
            _contols.StartButton.Click += StartExperimentAsync;
        }

        private async void StartExperimentAsync(object sender, EventArgs e)
        {
            PrepareUi();
            await RunExperimentAsync().ConfigureAwait(false);
            UnblockStartButton();
        }

        private async Task RunExperimentAsync()
        {
            var settings = ReadSettings();
            var experiment = new Experiment(settings);
            await foreach (var measuredTime in experiment.RunAsync().ConfigureAwait(false))
                _contols.Chart.AddPoint(measuredTime);
        }

        private void UnblockStartButton()
        {
            _contols.StartButton.UnblockButton();
        }

        private void PrepareUi()
        {
            _contols.StartButton.BlockButton();
            _contols.Chart.ResetSeries();
        }

        private Settings ReadSettings()
        {
            return new Settings()
            {
                StartBatch = _contols.StartBatchBox.GetValue(),
                BatchIncrement = _contols.BatchIncrementBox.GetValue(),
                InsertionsCount = _contols.InsertionsCountBox.GetValue(),
                InsertionIterance = _contols.IteranceBox.GetValue(),
                InsertionTypes = GetInsertionTypes(),
            };
        }

        private List<InsertionType> GetInsertionTypes()
        {
            var insertionTypes = new List<InsertionType>();
            if (_contols.EfCheckBox.Checked)
                insertionTypes.Add(InsertionType.EntityFramework);
            if (_contols.CompositeTypesCheckBox.Checked)
                insertionTypes.Add(InsertionType.CompositeTypes);
            if (_contols.BinaryCopyCheckBox.Checked)
                insertionTypes.Add(InsertionType.BinaryCopy);
            return insertionTypes;
        }
    }
}