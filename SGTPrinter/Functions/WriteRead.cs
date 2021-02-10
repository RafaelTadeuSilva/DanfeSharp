using System;
using System.IO;

namespace SGTPrinter.Functions
{
    public class Tools
    {
        public Tools()
        {
        }

        public static void WriteFile(string path, string texto)
        {
            File.WriteAllText(path, texto);
        }

        public static string ReadFile(string path, string fileName)
        {
            return File.ReadAllText(Path.Combine(path, fileName));
        }
    }
}
