using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class Vehicle
    {
        public static Vehicle CreateVehicle(string type, TaxParameters taxParameters)
        {
            if (taxParameters.TollFreeVehicleTypes.Any(x => x.Equals(type, StringComparison.InvariantCultureIgnoreCase)))
            {
                return new NonTaxableVehicle();
            }
            return new TaxableVehicle();
        }
    }
}