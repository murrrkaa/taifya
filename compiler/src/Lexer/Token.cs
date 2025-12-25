using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLexer
{
    public class Token
    {
        public Token(TokenType type, TokenValue? value = null)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; }

        public TokenValue? Value { get; }

        public override bool Equals(object? obj)
        {
            return obj is Token other && Type == other.Type && Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Type);
            if (Value is { } v)
            {
                sb.Append($" ({v})");
            }

            return sb.ToString();
        }
    }
}