using System.Diagnostics;
using System.Threading.Tasks;
using Billing.Database;
using Billing.Database.Utils;
using Billing.Models;

namespace Billing.Service.Experiment
{
    class RunCase
    {
        private readonly Call[] _calls;
        private readonly DbUtil _dbUtil;
        private readonly IInsert _insert;
        private readonly int _iterationsCount;

        public RunCase(IInsert insert, Call[] calls, int iterationsCount)
        {
            _calls = calls;
            _insert = insert;
            _dbUtil = new DbUtil();
            _iterationsCount = iterationsCount;
        }

        public async Task<double[]> MeasureTimeAsync()
        {
            await HeatUpMethodsAsync().ConfigureAwait(false);
            var results = new double[_iterationsCount];
            var sw = new Stopwatch();

            for (var i = 0; i < _iterationsCount; i++)
            {
                await MeasureTimeAsync(sw).ConfigureAwait(false);
                results[i] = sw.ElapsedMilliseconds;
                sw.Reset();
                await TruncateTableAsync().ConfigureAwait(false);
            }

            return results;
        }

        private Task TruncateTableAsync()
        {
            return _dbUtil.TruncateCallsAsync();
        }

        private async Task MeasureTimeAsync(Stopwatch sw)
        {
            sw.Start();
            await _insert.InsertAsync(_calls).ConfigureAwait(false);
            sw.Stop();
        }

        private async Task HeatUpMethodsAsync()
        {
            await _insert.InsertAsync(_calls).ConfigureAwait(false);
            await _dbUtil.TruncateCallsAsync().ConfigureAwait(false);
        }
    }
}