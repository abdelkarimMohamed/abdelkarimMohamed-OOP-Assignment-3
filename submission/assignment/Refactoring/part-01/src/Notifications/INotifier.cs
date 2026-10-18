namespace RefactoringLab.Notifications;

public interface INotifier
{
    void Send(NotificationMessage message);
}
