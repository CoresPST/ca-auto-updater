using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Portfolio
{
public static class Paths
{
public static string SafeCombine(string root, string relative)
        {
            if (String.IsNullOrEmpty(relative) || System.IO.Path.IsPathRooted(relative)) throw new InvalidDataException("Caminho absoluto bloqueado.");
            string full = System.IO.Path.GetFullPath(System.IO.Path.Combine(root, relative.Replace('/', '\\')));
            string prefix = System.IO.Path.GetFullPath(root).TrimEnd('\\') + "\\";
            if (!full.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Caminho fora da instalação.");
            string current = full;
            while (!String.Equals(current.TrimEnd('\\'), prefix.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
            {
                if ((File.Exists(current) || Directory.Exists(current)) && (File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidDataException("Link externo bloqueado: " + current);
                current = System.IO.Path.GetDirectoryName(current);
                if (current == null) throw new InvalidDataException("Caminho inválido.");
            }
            return full;
        }
}
}
