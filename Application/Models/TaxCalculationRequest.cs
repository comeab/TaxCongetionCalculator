using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class TaxCalculationRequest
    {
        public string Location;
        public IEnumerable<DateTime> TollEntryDates;
        public string VehicleType;
    }
}
