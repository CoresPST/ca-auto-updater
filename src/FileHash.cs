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
public static class FileHash
{
public static string Hash(string file)
        { using (var stream = File.OpenRead(file)) using (var sha = SHA256.Create()) return Hex(sha.ComputeHash(stream)); }

public static string Hex(byte[] bytes) { return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant(); }

public static bool ValidHash(string hash) { return hash != null && Regex.IsMatch(hash, "^[a-f0-9]{64}$"); }
}
}
