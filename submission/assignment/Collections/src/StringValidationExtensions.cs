using System.Text.RegularExpressions;

namespace CollectionsLab;

public static class StringValidationExtensions
{
    // 0 or +20, then 1, then one of 0/1/2/5, then exactly 8 digits.
    //  - \z instead of $  : $ also matches before a trailing "\n"
    //  - [0-9] instead of \d : \d also matches Arabic-Indic digits (٠١٢...)
    private static readonly Regex EgyptianPhone =
        new(@"^(?:0|\+20)1[0125][0-9]{8}\z", RegexOptions.Compiled);

    // First digit 2 or 3, then exactly 13 more digits = 14 digits.
    private static readonly Regex EgyptianNationalId =
        new(@"^[23][0-9]{13}\z", RegexOptions.Compiled);

    public static bool IsValidEgyptianPhone(this string? value) =>
        !string.IsNullOrWhiteSpace(value) && EgyptianPhone.IsMatch(value);

    public static bool IsValidEgyptianNationalId(this string? value) =>
        !string.IsNullOrWhiteSpace(value) && EgyptianNationalId.IsMatch(value);
}
