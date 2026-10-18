namespace RefactoringLab.Shipping;

public sealed class ShippingCostCalculator
{
    private readonly Dictionary<string, ICarrier> _carriers = new();

    public ShippingCostCalculator(IEnumerable<ICarrier> carriers)
    {
        foreach (var carrier in carriers)
        {
            if (!_carriers.TryAdd(carrier.Name, carrier))
                throw new ArgumentException($"Duplicate carrier: {carrier.Name}");
        }
    }

    public decimal Calculate(string carrier, decimal weightKg)
    {
        if (!_carriers.TryGetValue(carrier, out var found))
            throw new ArgumentException($"Unknown carrier: {carrier}");

        return found.Calculate(weightKg);
    }
}
