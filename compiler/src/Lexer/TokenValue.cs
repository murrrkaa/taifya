using System.Globalization;

namespace LanguageLexer;

public class TokenValue
{
    private readonly object value;

    public TokenValue(string value)
    {
        this.value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TokenValue(decimal value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return this.value switch
        {
            string s => s,
            decimal d => d.ToString(CultureInfo.InvariantCulture),
            _ => throw new NotImplementedException()
        };
    }

    public decimal ToDecimal()
    {
        return this.value switch
        {
            string s => decimal.Parse(s, CultureInfo.InvariantCulture),
            decimal d => d,
            _ => throw new NotImplementedException()
        };
    }

    public override bool Equals(object? obj)
    {
        if (obj is TokenValue other)
        {
            return this.value switch
            {
                string s => other.value is string os && s == os,
                decimal d => other.value is decimal od && d == od,
                _ => false
            };
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.value?.GetHashCode() ?? 0;
    }
}