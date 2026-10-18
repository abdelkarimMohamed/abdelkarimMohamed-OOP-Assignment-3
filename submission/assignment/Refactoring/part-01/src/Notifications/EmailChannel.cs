namespace RefactoringLab.Notifications;

public sealed class EmailChannel : INotifier
{
    public void Send(NotificationMessage m) =>
        Console.WriteLine($"[email{m.Tags}] {m.To}: {m.Body}");
}
