using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AndroidDebloater.Components;

public class WinScriptHandler
{
    public static string ExtractScript(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Debug: List available resources
        var resources = assembly.GetManifestResourceNames();

        // Validate resource existence
        if (!Array.Exists(resources, r => r.Equals(resourceName, StringComparison.Ordinal)))
        {
            throw new Exception($"Resource '{resourceName}' not found in the assembly. " +
                                $"Available resources: {string.Join(", ", resources)}");
        }

        // Extract the embedded resource
        string tempFile = Path.Combine(Path.GetTempPath(), "temp.bat");
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new NullReferenceException($"Resource stream for '{resourceName}' is null.");
            }

            using (FileStream fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        return tempFile;
    }

    public static string ExecuteBatchScript(string scriptPath)
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C \"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        try
        {
            process.Start();

            // Capture output
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine("Output: " + output);
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Error: " + error);
                return "Error: " + error;
            }
            return output;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return "An error occurred: " + ex.Message;
        }
    }
}