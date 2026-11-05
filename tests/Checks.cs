using System;
using System.IO;
using System.Threading;
using Portfolio;

class Checks
{
    static void Main()
    {
        string root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        try
        {
            string path = Path.Combine(root, "asset.dat");
            AtomicFiles.AtomicWrite(path, new byte[] { 1, 2, 3 });
            var entry = new ExpectedFile { Path = "asset.dat", Size = 3, Sha256 = FileHash.Hash(path) };
            if (UpdatePlan.ChangedFiles(root, new[] { entry }, CancellationToken.None).Length != 0) throw new Exception("Arquivo igual");
            AtomicFiles.AtomicWrite(path, new byte[] { 3, 2, 1 });
            if (UpdatePlan.ChangedFiles(root, new[] { entry }, CancellationToken.None).Length != 1) throw new Exception("Alteração com mesmo tamanho");
            File.Delete(path);
            if (UpdatePlan.ChangedFiles(root, new[] { entry }, CancellationToken.None).Length != 1) throw new Exception("Ausente");
            bool rejected = false;
            try { Paths.SafeCombine(root, "../fora.dat"); }
            catch (InvalidDataException) { rejected = true; }
            if (!rejected) throw new Exception("Caminho externo");
            rejected = false;
            try { UpdatePlan.ChangedFiles(root, new[] { entry, entry }, CancellationToken.None); }
            catch (InvalidDataException) { rejected = true; }
            if (!rejected) throw new Exception("Duplicado");
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                rejected = false;
                try { UpdatePlan.ChangedFiles(root, new[] { entry }, cancellation.Token); }
                catch (OperationCanceledException) { rejected = true; }
                if (!rejected) throw new Exception("Cancelamento");
            }
            using (var mutex = UpdateLock.Create(root))
            {
                if (!mutex.WaitOne(0)) throw new Exception("Mutex");
                mutex.ReleaseMutex();
            }
            Console.WriteLine("OK: arquivos iguais, alterados, ausentes, caminhos, duplicados e cancelamento");
        }
        finally { Directory.Delete(root, true); }
    }
}
