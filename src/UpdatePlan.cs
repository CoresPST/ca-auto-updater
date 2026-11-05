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
public sealed class ExpectedFile
{
    public string Path;
    public string Sha256;
    public long Size;
}
public static class UpdatePlan
{
    public static string[] ChangedFiles(string root, IEnumerable<ExpectedFile> files, CancellationToken cancellation)
    {
        var changed = new List<string>();
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var file in files)
        {
            cancellation.ThrowIfCancellationRequested();
            if (file == null || !names.Add(file.Path) || !FileHash.ValidHash(file.Sha256) || file.Size < 0)
                throw new InvalidDataException("Lista de arquivos inválida.");
            string target = Paths.SafeCombine(root, file.Path);
            if (!File.Exists(target) || new FileInfo(target).Length != file.Size || FileHash.Hash(target) != file.Sha256)
                changed.Add(file.Path);
        }
        return changed.ToArray();
    }
}
}
