namespace CurrencyChangeAPI.Models;

public class ChangeResponse
{
    public required string Message { get; set; }
    public required List<string> Change { get; set; }
}
