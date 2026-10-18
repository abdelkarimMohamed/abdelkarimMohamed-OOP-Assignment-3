namespace RefactoringLab.Orders;

public interface IOrderRepository
{
    void Save(int orderId, DateTime processedAt);
}
