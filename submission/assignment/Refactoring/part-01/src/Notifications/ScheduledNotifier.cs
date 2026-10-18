namespace RefactoringLab.Notifications;

// Decorator: wraps ANY notifier and adds the schedule to the header
public sealed class ScheduledNotifier : INotifier
{
    private readonly INotifier _inner;
    private readonly DateTime _sendAt;

    public ScheduledNotifier(INotifier inner, DateTime sendAt)
    {
        _inner = inner;
        _sendAt = sendAt;
    }

    public void Send(NotificationMessage m) =>
        _inner.Send(m with { Tags = $"{m.Tags} scheduled {_sendAt:g}" });
}
