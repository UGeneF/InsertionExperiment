using System;
using System.Collections.Generic;
using System.Linq;
using Billing.Models;

namespace Billing.Service.Experiment
{
    public class CallGenerator
    {
        public Call[] GetCalls(int count)
        {
            var random=new Random();
            return GenerateCalls(random, count)
                .ToArray();
        }

        private IEnumerable<Call> GenerateCalls(Random random,int count)
        {
            for (var i = 0; i < count; i++)
                yield return GenerateCall(random);
        }
        
        private Call GenerateCall(Random random)
        {
            var duration = random.Next(0, 1200);
            var startTime = GenerateStartTime(random);
            return new Call
            {
                StartTime = startTime,
                EndTime = startTime + TimeSpan.FromSeconds(duration),
                CallingNumber = GenerateNumber(random),
                CalledNumber = GenerateNumber(random),
                Duration = duration,
                CallType = (CallType) random.Next(0, 2),
                CallId = Guid.NewGuid().ToString()
            };
        }

        private DateTime GenerateStartTime(Random random)
        {
            return new DateTime(
                2000 + random.Next(16, 20),
                random.Next(1, 13),
                random.Next(1, 28),
                random.Next(0, 24),
                random.Next(0, 60),
                random.Next(0, 60));
        }

        private string GenerateNumber(Random random)
        {
            return (79000000000 +
                    random.Next(0, 99999) * 10000 +
                    random.Next(0, 9999))
                .ToString();
        }
    }
}