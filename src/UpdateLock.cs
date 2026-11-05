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
public static class UpdateLock
{
public static Mutex Create(string root)
        {
            string key;
            using (var sha = SHA256.Create()) key = FileHash.Hex(sha.ComputeHash(Encoding.UTF8.GetBytes(root.ToLowerInvariant()))).Substring(0, 24);
            return new Mutex(false, "Local\\SourceAdvanced.Update." + key);
        }
}
}
