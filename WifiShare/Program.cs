using System;
using System.Diagnostics;

namespace WifiShare
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = "wlan start hostednetwork"
                };

            var proc = Process.Start(psi);
            proc.WaitForExit();
            string errorOutput = proc.StandardError.ReadToEnd();
            string standardOutput = proc.StandardOutput.ReadToEnd();
            if (proc.ExitCode != 0)
                throw new Exception("netsh exit code: " + proc.ExitCode.ToString() + " " +
                                    (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " +
                                    (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : ""));
        }
    }
}
