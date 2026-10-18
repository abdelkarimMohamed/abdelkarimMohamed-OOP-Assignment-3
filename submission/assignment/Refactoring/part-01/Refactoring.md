# Part 01 — answers

---

## ShippingCostCalculator

- **What was the problem?**
  `Calculate` used a `switch` on the carrier name, with every price hard-coded inside it. Adding a carrier meant editing this class every time, which breaks the **Open/Closed Principle** (open for extension, closed for modification). One class also knew the pricing rules of every carrier.

- **What did you change?**
  I added an `ICarrier` interface (`Name` + `Calculate(weightKg)`) and moved each carrier into its own class (`AramexCarrier`, `FedExCarrier`, `DhlCarrier`). `ShippingCostCalculator` now receives the carriers in its constructor, stores them in a `Dictionary` by name, and looks the carrier up instead of branching. An unknown carrier still throws `ArgumentException` with the same message.

---

## OrderProcessor

- **What was the problem?**
  `Process` created `SqlOrderRepository` and `SmtpEmailSender` itself with `new`, so the high-level business logic depended directly on low-level details (SQL, SMTP). This breaks the **Dependency Inversion Principle**: you cannot swap the database or email provider, or test the class with fakes, without editing it. It also read `DateTime.Now` directly (a hidden dependency on the system clock), and read it twice, so the saved time and the emailed time could differ.

- **What did you change?**
  I added `IOrderRepository` and `IEmailSender`, made the existing classes implement them, and injected both through the constructor. I also injected `TimeProvider` (built into .NET) instead of calling `DateTime.Now`, and read the time once so both steps use the same timestamp. `OrderProcessor` now depends only on abstractions; `Program.cs` decides which implementations to use.

---

## Notifications

- **What was the problem?**
  Every feature combination was a new subclass: `UrgentEmail`, `UrgentSms`, `UrgentScheduledEmail`, `UrgentScheduledSms`... With *channels × options* the number of classes explodes (and `ScheduledEmail` / `ScheduledSms` without urgent did not even exist). The scheduled classes also copied the whole output line instead of reusing the parent, so the same logic was duplicated. Adding one channel would require a new class for every combination.

- **What did you change?**
  I used **composition over inheritance** with the **Decorator pattern**:
  - `INotifier` — one method `Send(NotificationMessage)`.
  - Channels: `EmailChannel`, `SmsChannel` — only know how to deliver.
  - Options as decorators: `UrgentNotifier` and `ScheduledNotifier` wrap *any* `INotifier` and change the message before passing it on.

  Options are now combined at runtime: `new UrgentNotifier(new ScheduledNotifier(new EmailChannel(), at))`. Two channels + two options = 4 small classes instead of one class per combination, and every channel automatically supports urgent, scheduled, or both.

---

## Proof

- **New carrier file(s):** `src/Shipping/BostaCarrier.cs`
- **New notification channel file(s):** `src/Notifications/WhatsAppChannel.cs`
- **Existing classes left unchanged? (yes/no):** yes — adding them only required the new files plus registering them in `Program.cs`. The output of the original `Program.cs` lines is identical to before.
