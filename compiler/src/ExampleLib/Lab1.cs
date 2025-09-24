using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleLib
{
    public static class Lab1
    {
        public static void AddLineNumbers(string path)
        {
            // Читаем и номируем строки.
            List<string> lines = File.ReadLines(path, Encoding.UTF8).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i] = $"{i + 1}. {lines[i]}";
            }

            // Перезаписываем файл с нуля (режим Truncate).
            using FileStream file = File.Open(path, FileMode.Truncate, FileAccess.Write);
            for (int i = 0, iMax = lines.Count; i < iMax; ++i)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(lines[i]);
                file.Write(bytes);
                if (i != iMax - 1)
                {
                    file.Write("\n"u8);
                }
            }
        }
    }
}
