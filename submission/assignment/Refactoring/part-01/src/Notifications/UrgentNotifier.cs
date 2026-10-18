namespace RefactoringLab.Notifications;

// Decorator: wraps ANY notifier and marks the body as urgent
public sealed class UrgentNotifier : INotifier
{
    private readonly INotifier _inner;

    public UrgentNotifier(INotifier inner) => _inner = inner;

    public void Send(NotificationMessage m) =>
        _inner.Send(m with { Body = $"[URGENT] {m.Body}" });
}
