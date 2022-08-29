using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Application.UnitTests
{
    public class TestData
    {
        private static readonly List<TaxTimeTable> taxTimeTable = new List<TaxTimeTable>
               {
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
         };
        public static TaxParameters GetTaxParameters()
        {
            var dummyResult = new TaxParameters
            {
                Location = "Gothenburg",
                MaxTaxChargeAmountPerDay = 60.0,
                MaxTaxPerDayEnabled = true,
                SingleTaxChargeRule = new SingleTaxChargeRule
                {
                    Enabled = true,
                    MaxFreeDurationTimeInMinutes = 60,
                    TaxOrder = "desc"
                },
                TaxPriceTimeTable = taxTimeTable,
                TollFreeDays = new List<DateTime>
                {
                    new DateTime(2022,12,25),
                },
                TollFreeVehicleTypes = new List<string>
                {
                    "Motorcycle",
                    "Tractor",
                    "Emergency",
                    "Diplomat",
                    "Foreign",
                    "Military"
                },
                IsWeekendsAlwaysTaxFree = true
            };

            return dummyResult;
        }

        public static List<DateTime> GetTollEntryDates()
        {
            return new List<DateTime>
            {
                //Out of hours
                DateTime.Parse("2013-01-14 21:00:00"), //0
                // Normal taxation
                DateTime.Parse("2013-02-07 06:23:27"), //8SEk
                DateTime.Parse("2013-02-07 15:27:00"), //13SEK
                //Max Tax charge per day
                DateTime.Parse("2013-02-08 06:27:00"),
                DateTime.Parse("2013-02-08 06:20:27"), //8SEK
                DateTime.Parse("2013-02-08 14:35:00"), //0 SEK
                DateTime.Parse("2013-02-08 15:29:00"), //13SEK
                DateTime.Parse("2013-02-08 15:47:00"), //18SEK
                DateTime.Parse("2013-02-08 16:01:00"), //0
                DateTime.Parse("2013-02-08 16:48:00"), //18SEK
                DateTime.Parse("2013-02-08 17:49:00"), //13SEK
                DateTime.Parse("2013-02-08 18:29:00"), //0
                DateTime.Parse("2013-02-08 18:35:00"), //0

                DateTime.Parse("2013-03-26 14:25:00"), //8SEK
                DateTime.Parse("2013-03-28 14:07:27"), //8SEK
                //Weekend Free Toll Day
                DateTime.Parse("2013-03-30 14:07:27") //0 SEK - Saturday
            };
        }

    }
}
