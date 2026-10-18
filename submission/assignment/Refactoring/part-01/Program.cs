using RefactoringLab.Notifications;
using RefactoringLab.Orders;
using RefactoringLab.Shipping;

// ---------------- Shipping ----------------
var shipping = new ShippingCostCalculator(new ICarrier[]
{
    new AramexCarrier(),
    new FedExCarrier(),
    new DhlCarrier(),
    new BostaCarrier(),          // new carrier — just registered here
});
Console.WriteLine($"Aramex 2kg → {shipping.Calculate("Aramex", 2)}");
Console.WriteLine($"FedEx 2kg  → {shipping.Calculate("FedEx", 2)}");
Console.WriteLine();

// ---------------- Orders ----------------
var processor = new OrderProcessor(new SqlOrderRepository(), new SmtpEmailSender(), TimeProvider.System);
processor.Process(1001, "customer@example.com");
Console.WriteLine();

// ---------------- Notifications ----------------
var at6pm = DateTime.Today.AddHours(18);

new UrgentNotifier(new ScheduledNotifier(new EmailChannel(), at6pm))
    .Send(new NotificationMessage("customer@example.com", "Your order ships tomorrow"));
new UrgentNotifier(new SmsChannel())
    .Send(new NotificationMessage("+201000000000", "OTP 4821"));

// ---------------- Proof: new carrier + new channel ----------------
Console.WriteLine();
Console.WriteLine("--- new carrier & channel ---");
Console.WriteLine($"Bosta 2kg  → {shipping.Calculate("Bosta", 2)}");

INotifier[] channels = { new EmailChannel(), new SmsChannel(), new WhatsAppChannel() };
foreach (var channel in channels)
{
    new UrgentNotifier(channel).Send(new NotificationMessage("user", "urgent only"));
    new ScheduledNotifier(channel, at6pm).Send(new NotificationMessage("user", "scheduled only"));
    new UrgentNotifier(new ScheduledNotifier(channel, at6pm)).Send(new NotificationMessage("user", "urgent + scheduled"));
}
