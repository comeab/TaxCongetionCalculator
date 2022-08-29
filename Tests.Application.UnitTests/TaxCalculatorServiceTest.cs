using Application;
using Application.Exceptions;
using Application.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Application.UnitTests
{
    public class TaxCalculatorServiceTest
    {
        private Mock<ITaxParametersService> _taxParametersService;
        private TaxCalculationService sut;

        [SetUp]
        public void Setup()
        {
            _taxParametersService = new Mock<ITaxParametersService>();
            sut = new TaxCalculationService(_taxParametersService.Object);
        }

        [Test]
        public void CalculatesTaxCorreclyForTaxableVehicles()
        {
           
            var expectedResult = new List<TaxCalculationResult>
            {
                new TaxCalculationResult
                {
                    Amount = 0.0,
                    Date = DateTime.Parse("2013-01-14"),
                    Reason = "Taxable Vehicle"
                },
                new TaxCalculationResult
                {
                    Amount = 21.0,
                    Date = DateTime.Parse("2013-02-07"),
                    Reason = "Taxable Vehicle"
                },
                new TaxCalculationResult
                {
                    Amount = 60.0,
                    Date = DateTime.Parse("2013-02-08"),
                    Reason = "Taxable Vehicle"
                },
            };
            var testParameters = TestData.GetTaxParameters();
            _taxParametersService.Setup(x => x.GetTaxParameters(It.IsAny<string>()))
                .ReturnsAsync(testParameters);

            var result = sut.CalculateTax(new TaxCalculationRequest()
            {
                Location = "Gothenburg",
                TollEntryDates = TestData.GetTollEntryDates(),
                VehicleType = "NormalTaxableVehicleType"
            }).Result.ToList();

            Assert.AreEqual(expectedResult[0].Amount, result[0].Amount);
            Assert.AreEqual(expectedResult[1].Amount, result[1].Amount);
            Assert.AreEqual(expectedResult[2].Amount, result[2].Amount);
        }

        [Test]
        public void ReturnsTaxZeroForNonTaxableVehicles()
        {
            var expectedResult = new List<TaxCalculationResult>
            {
                new TaxCalculationResult
                {
                    Amount = 0,
                    Date = null,
                    Reason = "NonTaxable Vehicle"
                }
            };
            _taxParametersService.Setup(x => x.GetTaxParameters(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetTaxParameters());

            var result = sut.CalculateTax(new TaxCalculationRequest()
            {
                Location = "Gothenburg",
                TollEntryDates = TestData.GetTollEntryDates(),
                VehicleType = "Emergency"
            }).Result.ToList();

            Assert.AreEqual(expectedResult[0].Amount, result[0].Amount);
            Assert.AreEqual(expectedResult[0].Date, result[0].Date);
            Assert.AreEqual(expectedResult[0].Reason, result[0].Reason);
        }

        [Test]
        public void ApplyMaxTaxChargeAmountPerDay()
        {
            var taxParamsInput = TestData.GetTaxParameters();
            taxParamsInput.SingleTaxChargeRule = null;
            var expectedResult = new List<TaxCalculationResult>
            {
                 new TaxCalculationResult
                {
                    Amount = taxParamsInput.MaxTaxChargeAmountPerDay,
                    Date = DateTime.Parse("2013-02-08"),
                    Reason = "Taxable Vehicle - Max Tax Amount Applied"
                },
            };


            _taxParametersService.Setup(x => x.GetTaxParameters(It.IsAny<string>()))
                .ReturnsAsync(taxParamsInput);

            var result = sut.CalculateTax(new TaxCalculationRequest()
            {
                Location = "Gothenburg",
                TollEntryDates = TestData.GetTollEntryDates(),
                VehicleType = "NormalTaxableVehicleType"
            }).Result.ToList();

            Assert.AreEqual(expectedResult[0].Amount, result[2].Amount);
            Assert.AreEqual(expectedResult[0].Date, result[2].Date);
            Assert.AreEqual(expectedResult[0].Reason, result[2].Reason);
        }
    }
}