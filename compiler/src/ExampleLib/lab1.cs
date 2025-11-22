using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExampleLib;

public static class Lab1
{
    /// <summary>
    /// Сортирует строки в указанном файле.
    /// Перезаписывает файл, но не атомарно: ошибка ввода-вывода при записи приведёт к потере данных.
    /// </summary>
    public static void AddLineNumbers(string path)
    {
        List<string> lines = new List<string>();
        foreach (string line in File.ReadLines(path))
        {
            lines.Add($"{lines.Count + 1}. {line}");
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