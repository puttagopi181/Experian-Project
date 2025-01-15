using Swashbuckle.AspNetCore.Filters;

namespace CurrencyChangeAPI.Swagger_Examples;

public class ChangeResponseExample : IExamplesProvider<ChangeResponse>
{
    public ChangeResponse GetExamples()
    {
        return new ChangeResponse
        {
            Message = "Your change is:",
            Change = new List<string>
                {
                    "1 x £20",
                    "1 x £2",
                    "1 x £1",
                    "1 x 50p",
                    "1 x 10p",
                    "1 x 5p"
                }
        };
    }
}
