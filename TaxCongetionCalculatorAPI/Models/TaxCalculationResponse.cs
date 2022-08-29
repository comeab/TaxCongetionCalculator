using Application.Models;
using System.Collections.Generic;

namespace TaxCongetionCalculatorAPI.Models
{
    public class TaxCalculationResponseModel
    {
        public IEnumerable<TaxCalculationResult> TaxationResult { get; set; }
    }
}
