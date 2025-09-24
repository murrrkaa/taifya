using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleLib
{
    public static class Lab2
    {
        public static string FormatEasternArabic(int value)
        {
            char[] estern = { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };
            string number;
            string result = "";

            if (value == int.MaxValue)
            {
                number = "2147483648";
            }
            else if (value == int.MinValue)
            {
                number = "-2147483648";
            }
            else
            {
                number = value.ToString();
            }

            foreach (char ch in number)
            {
                if (ch == '-')
                {
                    result += ch;
                }
                else if (ch >= '0' && ch <= '9')
                {
                    int digit = ch - '0';
                    result += estern[digit];
                }
            }
            return result;
        }
    }
}
