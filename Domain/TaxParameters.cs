using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class TaxParameters
    {
        public string Location { get; set; }
        public List<TaxTimeTable> TaxPriceTimeTable { get; set; }
        public bool MaxTaxPerDayEnabled { get; set; }
        public double MaxTaxChargeAmountPerDay { get; set; }
        public List<string> TollFreeVehicleTypes { get; set; }
        public List<DateTime> TollFreeDays { get; set; }
        public SingleTaxChargeRule SingleTaxChargeRule { get; set; }
        public bool IsWeekendsAlwaysTaxFree { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUpto { get; set; } // null means currently active
    }

    public class SingleTaxChargeRule
    {
        public bool Enabled { get; set; }
        public int MaxFreeDurationTimeInMinutes { get; set; }
        public string TaxOrder { get; set; }
    }

    public class TaxTimeTable
    {
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public double Amount { get; set; }
    }
}