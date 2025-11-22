using System;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLexer
{
    public class TestScanner
    {
        private readonly string text;
        private int position;

        public TestScanner(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            position = 0;
        }

        public char Peek(int n = 0)
        {
            int pos = position + n;
            return pos >= text.Length ? '\0' : text[pos];
        }

        public void Advance()
        {
            position++;
        }

        public bool IsEnd()
        {
            return position >= text.Length;
        }
    }
}