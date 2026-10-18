namespace RefactoringLab.Shipping;

// NEW carrier — added without editing any existing class
public sealed class BostaCarrier : ICarrier
{
    public string Name => "Bosta";
    public decimal Calculate(decimal weightKg) => weightKg * 10m;
}
