namespace RefactoringLab.Shipping;

public interface ICarrier
{
    string Name { get; }
    decimal Calculate(decimal weightKg);
}
