using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxCongetionCalculatorAPI.Models
{
    public class TaxCalculationRequestModel
    {
        [Required]
        public string VehicleType { get; set; }
        [Required]
        public IEnumerable<DateTime> TollEntryDates { get; set; }
        [Required]
        public string Location { get; set; }

        public TaxCalculationRequest ToDomain()
        {
            return new TaxCalculationRequest
            {
                Location = this.Location,
                TollEntryDates = this.TollEntryDates,
                VehicleType = this.VehicleType
            };
        }
    }
}
