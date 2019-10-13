using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Models
{
    [Table("calls")]
    public class Call
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CallingNumber { get; set; }
        public string CalledNumber { get; set; }
        public int Duration { get; set; }
        public CallType CallType { get; set; }
        [Key] public string CallId { get; set; }
    }
}