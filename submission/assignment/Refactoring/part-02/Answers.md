# Part 02 — answers

---

## Reports

- **What was the problem?**
  `CsvReportExporter`, `JsonReportExporter` and `TextReportExporter` were three copies of the same class. `Export`, `Load`, `Validate` and `Save` were identical in all three; only `Format` was different. Any change to the flow (a new validation rule, a different data source, logging) had to be made three times, and the copies could easily drift apart.

- **What did you change?**
  I applied the **Template Method** pattern with an abstract class `ReportExporter`:
  - `Export` is the template method: it defines the steps and their order (Load → Validate → Format → Save) in one place. It is not virtual, so subclasses cannot change the order.
  - `Load`, `Validate` and `Save` are written once in the base class.
  - `Format` is `protected abstract` — the only step each exporter implements.

  Each exporter is now a few lines that contain only its own format. The three files are written with the same content as before (verified by comparing the files before and after).

- **Why did you choose that approach? (Why an abstract class and not an interface?)**
  An interface can only declare *what* methods exist; it cannot hold shared state or force an order of steps. The whole point here is to share real code (Load, Validate, Save) and to **lock the algorithm's order** in one method while letting subclasses fill in one step. An abstract class can do all three: provide implemented methods, keep them `private` so subclasses cannot break them, and mark `Format` as `abstract` so every exporter must provide it. With an interface, every exporter would still need to re-implement (or copy) the steps and the order — which is exactly the duplication we wanted to remove. (Default interface methods exist, but they can't be private helpers that hide the flow, and they are meant for evolving interfaces, not for sharing an algorithm.)

---

## Enrollment

- **What was the problem?**
  To enroll one student, `Program.cs` had to create four services (`PaymentGateway`, `SeatInventory`, `InvoiceGenerator`, `EmailService`) and call them one by one in the correct order, passing the invoice id from one call into the next. The caller was coupled to every service and to the business rule "charge → reserve → invoice → email". Every place that enrolls a student would repeat this, and a mistake in the order (e.g. reserving a seat before payment) would be easy to make.

- **What did you change?**
  I added an `EnrollmentFacade` class with one method: `Enroll(studentId, courseId, amount)`. It owns the four services and calls them in the right order, then returns the invoice id. `Program.cs` now enrolls a student with one line and no longer creates or calls any of the four services. `Services.cs` was not changed, and the output is the same.

- **Why did you choose that approach? (What does the caller no longer need to know?)**
  The Facade gives a simple front door to a complicated subsystem. The caller no longer needs to know:
  - which services take part in an enrollment, or how to create them;
  - the order of the steps;
  - that the invoice id from `InvoiceGenerator` must be passed into the email.

  If the enrollment flow changes later (e.g. add a waiting list), only the Facade changes — not every caller. The services are still public, so advanced code can use them directly if needed; the Facade doesn't hide them, it just makes the common case simple. A second constructor accepts the services, so they can be replaced (e.g. with fakes in tests).
