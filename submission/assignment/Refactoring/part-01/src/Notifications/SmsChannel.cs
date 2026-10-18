namespace RefactoringLab.Notifications;

public sealed class SmsChannel : INotifier
{
    public void Send(NotificationMessage m) =>
        Console.WriteLine($"[sms{m.Tags}] {m.To}: {m.Body}");
}
