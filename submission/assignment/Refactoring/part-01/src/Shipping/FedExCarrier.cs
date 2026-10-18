namespace RefactoringLab.Shipping;

public sealed class FedExCarrier : ICarrier
{
    public string Name => "FedEx";
    public decimal Calculate(decimal weightKg) => weightKg * 15m;
}
