using Application.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class TaxParametersService : ITaxParametersService
    {
        /// <summary>
        ///   Get city parameters from external API service or db repository
        /// </summary>
        /// <param name="city">Location configured</param>
        /// <returns>List of parameters to consider for tax calculation</returns>
        public Task<TaxParameters> GetTaxParameters(string city)
        {
            // Load parameters from database or external API

            if (city != "Gothenburg")
            {
                throw new TaxParametersNotFoundException();
            }
            var gothenburgParameters = new TaxParameters
            {
                Location = city,
                MaxTaxChargeAmountPerDay = 60.0,
                MaxTaxPerDayEnabled = true,
                SingleTaxChargeRule = new SingleTaxChargeRule
                {
                    Enabled = true,
                    MaxFreeDurationTimeInMinutes = 60,
                    TaxOrder = "desc"
                },
                TaxPriceTimeTable = new List<TaxTimeTable>
                {
                    //only the time matters
                    new TaxTimeTable
                    {
                        From = new TimeSpan(6, 00, 00),
                        To = new TimeSpan(6, 29, 59),
                        Amount = 8.0
                    },
                    new TaxTimeTable
                    {
                        From = new TimeSpan(6, 30, 00),
                        To = new TimeSpan(6, 59, 59),
                        Amount = 13.0
                    },
                     new TaxTimeTable
                    {
                        From = new TimeSpan(7, 00, 00),
                        To = new TimeSpan(7, 59, 00),
                        Amount = 18.0
                    },
                    new TaxTimeTable
                    {
                        From = new TimeSpan(8, 00, 00),
                        To = new TimeSpan(8, 29, 00),
                        Amount = 13.0
                    },
                    new TaxTimeTable
                    {
                        From = new TimeSpan(8, 30, 00),
                        To = new TimeSpan(14, 59, 00),
                        Amount = 8.0
                    },
                      new TaxTimeTable
                    {
                        From = new TimeSpan(15, 00, 00),
                        To = new TimeSpan(15, 29, 59),
                        Amount = 13.0
                    },
                        new TaxTimeTable
                    {
                        From = new TimeSpan(15, 30, 00),
                        To = new TimeSpan(16, 59, 59),
                        Amount = 13.0
                    },
                        new TaxTimeTable
                    {
                        From = new TimeSpan(17, 00, 00),
                        To = new TimeSpan(17, 59, 59),
                        Amount = 13.0
                    },
                            new TaxTimeTable
                    {
                        From = new TimeSpan(18, 00, 00),
                        To = new TimeSpan(18, 29, 59),
                        Amount = 13.0
                    }
                },
                TollFreeDays = new List<DateTime>
                {
                    new DateTime(1999, 1, 1),
                    new DateTime(1999, 12, 25),
                    new DateTime(1999, 12, 31),
                    //add other public holidays
                },
                IsWeekendsAlwaysTaxFree = true,
                TollFreeVehicleTypes = new List<string>
                {
                    "Motorcycle",
                    "Tractor",
                    "Emergency",
                    "Diplomat",
                    "Foreign",
                    "Military"
                }
            };

            return Task.FromResult(gothenburgParameters);
        }
    }
}
