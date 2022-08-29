using Application;
using Application.Exceptions;
using Application.Models;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaxCongetionCalculatorAPI.Models;

namespace TaxCongetionCalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculationController : ControllerBase
    {
        private readonly ILogger<TaxCalculationController> _logger;
        private readonly ITaxCalculationService _taxCalculationService;


        public TaxCalculationController(ITaxCalculationService taxCalculationService, ILogger<TaxCalculationController> logger)
        {
            _logger = logger;
            _taxCalculationService = taxCalculationService;
        }

        [HttpPost]
        public async Task<ActionResult<TaxCalculationResponseModel>> Post([FromBody]TaxCalculationRequestModel request)
        {
            try
            {
                if (request.Location == null || request.Location == string.Empty) return BadRequest("Location cannot be empty");
                if (request.VehicleType ==null || request.VehicleType == string.Empty) return BadRequest("Vehicle type cannot be empty");

                var taxResult = await this._taxCalculationService.CalculateTax(request.ToDomain());

                return Ok( new TaxCalculationResponseModel { TaxationResult = taxResult });
            }
            catch (TaxParametersNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }
    }
}
