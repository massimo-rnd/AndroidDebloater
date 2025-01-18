using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace AndroidDebloater.Components;

public class ShellExecutor
{
    
    public static string ListADB()
    {
        string command = "\"adb devices\"";
        return AdbHelper.ExecuteAdbCommand("devices"); //Execute(command);
    }

    public static string StartDebloat(int package)
    {
        switch (package)
            {
                case 1:
                    //Google
                    return ExecuteScript("google");
                    break;
                case 2:
                    //AndroidSystem
                    return ExecuteScript("system");
                    break;
                case 3:
                    //ThirdParty
                    return ExecuteScript("thirdparty");
                    break;
                case 4:
                    //Google Manufacturer
                    return ExecuteScript("googlem");
                    break;
                case 5:
                    //Huawei Manufacturer
                    return ExecuteScript("huawei");
                    break;
                case 6:
                    //OnePlus Manufacturer
                    return ExecuteScript("oneplus");
                    break;
                case 7:
                    //Oppo Manufacturer
                    return ExecuteScript("oppo");
                    break;
                case 8:
                    //Realme Manufacturer
                    return ExecuteScript("realme");
                    break;
                case 9:
                    //Samsung Manufacturer
                    return ExecuteScript("samsung");
                    break;
                case 10:
                    //Vivo Manufacturer
                    return ExecuteScript("vivo");
                    break;
                case 11:
                    //Xiaomi Manufacturer
                    return ExecuteScript("xiaomi");
                    break;
                default:
                    //nothing
                    break;
            }

        return "";
    }
    
    public static string ExecuteScript(string scriptName)
    {
        string scriptPath = string.Empty;
        string adbPath = AdbHelper.GetAdbPath(); // Get the bundled adb path

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "Scripts", "windows", scriptName + ".bat");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "Scripts", "bash", scriptName + ".sh");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components", "Scripts", "bash", scriptName + ".sh");
        }

        if (!File.Exists(scriptPath))
        {
            throw new FileNotFoundException("Script not found: " + scriptPath);
        }

        var processInfo = new ProcessStartInfo
        {
            FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd.exe" : "/bin/bash",
            Arguments = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? $"/C \"\"{scriptPath}\" \"{adbPath}\"\""
                : $"\"{scriptPath}\" \"{adbPath}\"",
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
                Console.Error.WriteLine($"Script Error: {error}");
                return $"Script Error: {error}";
            }
            else
            {
                Console.WriteLine(output);
                return output;
            }
        }
    }
    
}