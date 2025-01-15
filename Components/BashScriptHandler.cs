using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AndroidDebloater.Components;

public class BashScriptHandler
{
    public static string ExtractScript(string resourceName)
    {
        // Get the executing assembly
        var assembly = Assembly.GetExecutingAssembly();

        var resources = assembly.GetManifestResourceNames();

        // Ensure the resource name exists
        if (!Array.Exists(resources, r => r.Equals(resourceName)))
        {
            throw new Exception($"Resource '{resourceName}' not found in the assembly.");
        }

        // Extract the embedded resource
        string tempFile = Path.Combine(Path.GetTempPath(), "temp.sh");
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new NullReferenceException($"Resource stream for '{resourceName}' is null.");

            using (FileStream fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        // Ensure the file is executable
        Process chmod = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "chmod",
                Arguments = $"+x \"{tempFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        chmod.Start();
        chmod.WaitForExit();

        return tempFile;
    }

    public static string ExecuteBashScript(string scriptPath)
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"\"{scriptPath}\"",
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