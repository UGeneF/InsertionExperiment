using System.Collections.Generic;

namespace Billing.Service.Experiment.Models
{
    public class Settings
    {
        public List<InsertionType> InsertionTypes { get; set; }
        public int StartBatch { get; set; }
        public int BatchIncrement { get; set; }
        public int InsertionsCount { get; set; }
        public int InsertionIterance { get; set; }
    }
}