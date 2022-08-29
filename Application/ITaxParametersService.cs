using Domain.Models;
using System.Threading.Tasks;

namespace Application
{
    public interface ITaxParametersService
    {
        public Task<TaxParameters> GetTaxParameters(string city);
    }
}
