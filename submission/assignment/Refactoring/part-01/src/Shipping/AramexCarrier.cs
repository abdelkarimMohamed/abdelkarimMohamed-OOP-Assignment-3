namespace RefactoringLab.Shipping;

public sealed class AramexCarrier : ICarrier
{
    public string Name => "Aramex";
    public decimal Calculate(decimal weightKg) => weightKg * 12m;
}
