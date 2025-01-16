using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace AndroidDebloater.Components;

public class AdbHelper
{
    public static string GetAdbPath()
    {
        string adbPath = string.Empty;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "adb", "windows", "adb.exe");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "adb", "macos", "adb");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "adb", "linux", "adb");
        }

        if (!File.Exists(adbPath))
        {
            throw new FileNotFoundException("ADB binary not found: " + adbPath);
        }

        return adbPath;
    }

    public static string ExecuteAdbCommand(string arguments)
    {
        string adbPath = GetAdbPath();

        var processInfo = new ProcessStartInfo
        {
            FileName = adbPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = Process.Start(processInfo))
        {
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(error))
            {
                Console.Error.WriteLine($"ADB Error: {error}");
                return $"ADB Error: {error}";
            }
            else
            {
                Console.WriteLine(output);
                return output;
            }
        }
    }
}