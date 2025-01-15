namespace CurrencyChangeAPI.Utilities;

public static class ChangeCalculator
{
    public static List<string> CalculateChange(decimal paidAmount, decimal productPrice)
    {
        var denominations = new Dictionary<decimal, string>
            {
                { 50m, "£50" },
                { 20m, "£20" },
                { 10m, "£10" },
                { 5m, "£5" },
                { 2m, "£2" },
                { 1m, "£1" },
                { 0.50m, "50p" },
                { 0.20m, "20p" },
                { 0.10m, "10p" },
                { 0.05m, "5p" },
                { 0.02m, "2p" },
                { 0.01m, "1p" }
            };

        var changeAmount = paidAmount - productPrice;
        var result = new List<string>();

        foreach (var denom in denominations)
        {
            var count = (int)(changeAmount / denom.Key);
            if (count > 0)
            {
                result.Add($"{count} x {denom.Value}");
                changeAmount -= count * denom.Key;
            }
        }

        return result;
    }
}
