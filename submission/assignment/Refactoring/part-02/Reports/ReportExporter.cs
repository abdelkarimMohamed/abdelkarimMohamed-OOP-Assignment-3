namespace RefactoringLab.Part02.Reports;

// Template Method: the export steps and their order live here — and only here.
public abstract class ReportExporter
{
    // The template method. Not virtual, so no subclass can change the order.
    public void Export(string path)
    {
        var rows = Load();
        if (!Validate(rows))
            throw new InvalidOperationException("Invalid data");
        var content = Format(rows);
        Save(path, content);
    }

    // Shared steps — written once.
    private List<string[]> Load() =>
    [
        ["Id", "Name"],
        ["1", "Keyboard"],
        ["2", "Mouse"]
    ];

    private bool Validate(List<string[]> rows) =>
        rows.Count > 1 && rows[0].Length > 0;

    private void Save(string path, string content) =>
        File.WriteAllText(path, content);

    // The only step that differs between exporters.
    protected abstract string Format(List<string[]> rows);
}
