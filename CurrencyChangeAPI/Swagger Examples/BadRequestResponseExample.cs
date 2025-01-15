using Swashbuckle.AspNetCore.Filters;

namespace CurrencyChangeAPI.Swagger_Examples
{
    public class BadRequestResponseExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            return "Paid amount is less than the product price.";
        }
    }
}
