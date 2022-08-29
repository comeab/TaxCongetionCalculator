using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application
{
    public interface ITaxCalculationService
    {
        public Task<IEnumerable<TaxCalculationResult>> CalculateTax(TaxCalculationRequest request);
    }
}
