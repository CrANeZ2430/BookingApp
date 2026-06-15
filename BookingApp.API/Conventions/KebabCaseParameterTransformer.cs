using System.Text.RegularExpressions;

namespace BookingApp.API.Conventions;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) return null;
        return Regex.Replace(value.ToString()!, "([a-z0-9])([A-Z])", "$1-$2").ToLower();
    }
}