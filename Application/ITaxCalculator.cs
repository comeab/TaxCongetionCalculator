using Application.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface ITaxCalculator
    {
        public IEnumerable<TaxCalculationResult> CalculateTax();
    }
}
