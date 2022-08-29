using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Models;
using Domain.Models;

namespace Application.CongestionTaxCalculation
{

    public class TaxCalculator: ITaxCalculator
    {
        private readonly Vehicle vehicle;
        private readonly TaxParameters taxParameters;
        private readonly IEnumerable<DateTime> entryDates;

        public TaxCalculator(string vehicleType, TaxParameters taxParameters, IEnumerable<DateTime> entryDates)
        {
            this.vehicle = Vehicle.CreateVehicle(vehicleType, taxParameters);
            this.taxParameters = taxParameters;
            this.entryDates = entryDates.OrderBy(date => date).ToList();
        }

        public IEnumerable<TaxCalculationResult> CalculateTax()
        {
            var taxCalculationResult = new List<TaxCalculationResult>();

            if (!IsTaxableVehicle())
            {
                taxCalculationResult.Add(new TaxCalculationResult
                {
                    Amount = 0.0,
                    Reason = "NonTaxable Vehicle"
                });

                return taxCalculationResult;
            }

            var groupedTimesByDay = this.entryDates
                                .GroupBy(x => new { Day = x.Date })
                                .Select(groupedByDay => new GroupedDateTime
                                {
                                    Day = groupedByDay.Key.Day,
                                    EntryTimes = groupedByDay.ToList()
                                })
                                .ToList();

            foreach (var entryDay in groupedTimesByDay)
            {
                if (IsTollFreeDate(entryDay.Day))
                {
                    taxCalculationResult.Add(new TaxCalculationResult
                    {
                        Amount = 0.0,
                        Date = entryDay.Day,
                        Reason = "Toll Free Day"
                    });
                }
                else
                {
                    var calculatedTax = CalculateTaxPerDay(entryDay.EntryTimes);
                    taxCalculationResult.Add(calculatedTax);
                }
            }

            return taxCalculationResult;
        }

        private TaxCalculationResult CalculateTaxPerDay(List<DateTime> entryDates)
        {
            var lastTaxedTollTime = entryDates[0];
            var totalTax = 0.0;
            var previousAmountToTax = 0.0;

            foreach (var currentDateToTax in entryDates)
            {
                var amountToTax = GetTaxAmountFromPriceTable(currentDateToTax);

                if (taxParameters.SingleTaxChargeRule != null && taxParameters.SingleTaxChargeRule.Enabled)
                {
                    TimeSpan timePast = currentDateToTax - lastTaxedTollTime;
                    long minutesPast = (long)timePast.TotalMilliseconds / 1000 / 60;
                    if (minutesPast <= taxParameters.SingleTaxChargeRule.MaxFreeDurationTimeInMinutes)
                    {
                        if (amountToTax > previousAmountToTax)
                        {
                            var highestTaxAmount = amountToTax;
                            amountToTax -= previousAmountToTax;
                            previousAmountToTax = highestTaxAmount;                         
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        lastTaxedTollTime = currentDateToTax;
                    }
                }
                
                totalTax += amountToTax;
                if (taxParameters.MaxTaxPerDayEnabled)
                {
                    if (totalTax > taxParameters.MaxTaxChargeAmountPerDay)
                    {
                        totalTax = taxParameters.MaxTaxChargeAmountPerDay;
                        return new TaxCalculationResult
                        {
                            Date = lastTaxedTollTime.Date,
                            Amount = totalTax,
                            Reason = "Taxable Vehicle - Max Tax Amount Applied"
                        };
                    }
                }
            }

            return new TaxCalculationResult
            {
                Date = lastTaxedTollTime.Date,
                Amount = totalTax,
                Reason = "Taxable Vehicle"
            };
        }

        private bool IsTaxableVehicle()
        {
            if (this.vehicle is NonTaxableVehicle) return false;
            return true;
        }

        private double GetTaxAmountFromPriceTable(DateTime date)
        {
            TimeSpan time = date.TimeOfDay;
            foreach (var price in this.taxParameters.TaxPriceTimeTable)
            {
                if (time >= price.From && time <= price.To)
                    return price.Amount;
            }
            return 0.0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;
            if (this.taxParameters.IsWeekendsAlwaysTaxFree)
            {
                if (date.DayOfWeek.Equals(DayOfWeek.Saturday) || date.DayOfWeek.Equals(DayOfWeek.Sunday)) return true; 
            }

            if (this.taxParameters.TollFreeDays.Any(d => d.Month == month && d.Day == day)) return true;

            return false;
        }

        private class GroupedDateTime
        {
            public DateTime Day;
            public List<DateTime> EntryTimes;
        }
    }
}