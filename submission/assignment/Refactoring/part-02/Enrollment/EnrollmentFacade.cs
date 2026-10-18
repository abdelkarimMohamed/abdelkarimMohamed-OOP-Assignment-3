namespace RefactoringLab.Part02.Enrollment;

// Facade: one simple entry point in front of the four enrollment services.
public sealed class EnrollmentFacade
{
    private readonly PaymentGateway _payment;
    private readonly SeatInventory _seats;
    private readonly InvoiceGenerator _invoices;
    private readonly EmailService _email;

    public EnrollmentFacade()
        : this(new PaymentGateway(), new SeatInventory(), new InvoiceGenerator(), new EmailService())
    {
    }

    public EnrollmentFacade(PaymentGateway payment, SeatInventory seats,
                            InvoiceGenerator invoices, EmailService email)
    {
        _payment = payment;
        _seats = seats;
        _invoices = invoices;
        _email = email;
    }

    // The correct order is known in one place only.
    public string Enroll(string studentId, string courseId, decimal amount)
    {
        _payment.Charge(studentId, amount);
        _seats.Reserve(courseId, studentId);
        var invoiceId = _invoices.Create(studentId, amount);
        _email.Send(studentId, "Enrollment confirmed", $"Invoice {invoiceId} for {courseId}");
        return invoiceId;
    }
}
