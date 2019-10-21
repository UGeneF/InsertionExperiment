using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.Database;
using Billing.Database.BinaryCopy;
using Billing.Database.CompositeTypes;
using Billing.Database.EntityFramework;
using Billing.Models;
using Billing.Service.Experiment.Models;

namespace Billing.Service.Experiment
{
    public class Experiment
    {
        private readonly Settings _settings;
        private readonly CallGenerator _callGenerator;
        private readonly BinaryInsert _binaryInsert = new BinaryInsert();
        private readonly CompositeTypesInsert _compositeTypesInsert = new CompositeTypesInsert();
        private readonly EntityFrameworkInsert _entityFrameworkInsert = new EntityFrameworkInsert();

        public Experiment(Settings settings)
        {
            _settings = settings;
            _callGenerator = new CallGenerator();
        }

        public async IAsyncEnumerable<MeasuredTime> RunAsync()
        {
            for (int batchSize = _settings.StartBatch, i = 0;
                i < _settings.InsertionsCount;
                batchSize += _settings.BatchIncrement,i++)
            {
                var calls = GenerateCalls(batchSize);
                foreach (var insertionType in _settings.InsertionTypes)
                    yield return await GetMeasuredTimeAsync(insertionType, calls, batchSize)
                        .ConfigureAwait(false);
            }
        }

        private async Task<MeasuredTime> GetMeasuredTimeAsync(InsertionType insertionType, Call[] calls, int batchSize)
        {
            var measuredIterations = await MeasureTimeAsync(insertionType, calls).ConfigureAwait(false);
            var average = measuredIterations.Average();
            return new MeasuredTime()
            {
                InsertionType = insertionType,
                Average = average,
                BatchSize = batchSize
            };
        }

        private async Task<double[]> MeasureTimeAsync(InsertionType insertionType, Call[] calls)
        {
            var insert = GetInsert(insertionType);
            var runCase = GetRunCase(insert, calls);
            var measuredIterations = await runCase.MeasureTimeAsync().ConfigureAwait(false);
            return measuredIterations;
        }

        private RunCase GetRunCase(IInsert insert, Call[] calls)
        {
            return new RunCase(insert, calls, _settings.InsertionIterance);
        }

        private Call[] GenerateCalls(int batchSize)
        {
            return _callGenerator.GetCalls(batchSize);
        }

        private IInsert GetInsert(InsertionType insertionType)
        {
            switch (insertionType)
            {
                case InsertionType.EntityFramework:
                    return _entityFrameworkInsert;
                case InsertionType.CompositeTypes:
                    return _compositeTypesInsert;
                case InsertionType.BinaryCopy:
                    return _binaryInsert;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}