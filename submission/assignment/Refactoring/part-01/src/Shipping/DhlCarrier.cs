namespace RefactoringLab.Shipping;

public sealed class DhlCarrier : ICarrier
{
    public string Name => "DHL";
    public decimal Calculate(decimal weightKg) => weightKg * 18m;
}
