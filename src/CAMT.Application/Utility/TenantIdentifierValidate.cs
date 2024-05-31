using System.Text.RegularExpressions;

namespace CAMT.Application.Utility;

public class TenantIdentifierValidate
{
    public static bool Validate(string input)
    {
        string pattern = @"^[a-zA-Z0-9]*$";
        return Regex.IsMatch(input, pattern);
    }
}