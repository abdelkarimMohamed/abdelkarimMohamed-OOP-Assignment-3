namespace RefactoringLab.Orders;

public sealed class OrderProcessor
{
    private readonly IOrderRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly TimeProvider _clock;

    public OrderProcessor(IOrderRepository repository, IEmailSender emailSender, TimeProvider clock)
    {
        _repository = repository;
        _emailSender = emailSender;
        _clock = clock;
    }

    public void Process(int orderId, string customerEmail)
    {
        var now = _clock.GetLocalNow().LocalDateTime; // one timestamp for both steps

        _repository.Save(orderId, now);
        _emailSender.Send(customerEmail, $"Order {orderId} confirmed at {now}");
    }
}
