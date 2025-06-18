using System.Text.RegularExpressions;

namespace UserManagementApp.Presentation;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value is null
            ? null
            : Regex.Replace(input: value.ToString() ?? String.Empty,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant)
            .ToLowerInvariant();
    }
}
