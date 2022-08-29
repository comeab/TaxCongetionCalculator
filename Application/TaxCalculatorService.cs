using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models;
using Domain.Models;

namespace Application
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly ITaxParametersService _taxParameterService; //or repository to fetch from db
        public TaxCalculationService(ITaxParametersService taxParameterService)
        {
            _taxParameterService = taxParameterService;
        }

        public async Task<IEnumerable<TaxCalculationResult>> CalculateTax(TaxCalculationRequest request)
        {
            var taxParameters = await this._taxParameterService.GetTaxParameters(request.Location);

            if(taxParameters == null)
            {
                throw new TaxParametersNotFoundException($"Tax parameters not found for {request.Location}");
            }

            var calculator = new CongestionTaxCalculation.TaxCalculator(request.VehicleType, taxParameters, request.TollEntryDates);

            return calculator.CalculateTax();
        }

    }
}
