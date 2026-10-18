namespace RefactoringLab.Notifications;

// NEW channel — added without editing any existing class
public sealed class WhatsAppChannel : INotifier
{
    public void Send(NotificationMessage m) =>
        Console.WriteLine($"[whatsapp{m.Tags}] {m.To}: {m.Body}");
}
