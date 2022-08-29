using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class TaxCalculationResult
    {
        public DateTime? Date { get; set; }
        public double Amount { get; set; }
        public string Reason { get; set; }

    }
}
