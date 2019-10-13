namespace Billing.Service.Experiment.Models
{
    public class MeasuredTime
    {
        public int BatchSize { get; set; }
        public double Average { get; set; }
        public InsertionType InsertionType { get; set; }
    }
}