namespace RefactoringLab.Notifications;

// Tags = extra text shown in the header, e.g. " scheduled 10/14/2026 18:00"
public sealed record NotificationMessage(string To, string Body, string Tags = "");
