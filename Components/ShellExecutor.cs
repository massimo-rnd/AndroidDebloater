using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AndroidDebloater.Components;

public class ShellExecutor
{
    
    public static string ListADB()
    {
        string command = "\"adb devices\"";
        return Execute(command);
    }

    public static string StartDebloat(int package)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            switch (package)
            {
                case 1:
                    //Google BAT
                    //string scriptFile = WinScriptHandler.ExtractScript("AndroidDebloater.Components.Scripts.test.bat");
                    
                    return WinScriptHandler.ExecuteBatchScript(WinScriptHandler.ExtractScript("AndroidDebloater.Components.Scripts.test.bat"));
                    break;
                case 2:
                    //AndroidSystem BAT
                    return Execute(DebloatScripts.aDebloat);
                    break;
                case 3:
                    //ThirdParty BAT
                    return Execute(DebloatScripts.tpDebloat);
                    break;
                case 4:
                    //FullDebloat BAT
                    return Execute(DebloatScripts.fullDebloat);
                    break;
                default:
                    //nothing
                    break;
            }
        }else
        {
            switch (package)
            {
                case 1:
                    //Google Debloat BASH
                    return BashScriptHandler.ExecuteBashScript(
                        BashScriptHandler.ExtractScript("AndroidDebloater.Components.Scripts.test.sh"));
                    break;
                case 2:
                    //AndroidSystem BASH
                    return Execute(DebloatScripts.aDebloatBash);
                    break;
                case 3:
                    //ThirdParty BASH
                    return Execute(DebloatScripts.tpDebloatBash);
                    break;
                case 4:
                    //FullDebloat BASH
                    return Execute(DebloatScripts.fullDebloatBash);
                    break;
                default:
                    //nothing
                    break;
            }
        }

        return "";
    }
    
    public static string Execute(string command)
    {
        Process shell = new Process();
        //get OS Platform for Shell type
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            shell.StartInfo.FileName = "cmd.exe"; // Use "bash" on Linux/MacOS
            shell.StartInfo.Arguments = $"/C {command}"; // "/C" runs the command and exits
        }
        else
        {
            shell.StartInfo.FileName = "bash"; // Use "bash" on Linux/MacOS
            shell.StartInfo.Arguments = $"-c {command}"; // "/C" runs the command and exits
        }
        
        shell.StartInfo.RedirectStandardOutput = true; // Redirect output
        shell.StartInfo.RedirectStandardError = true; // Redirect error output
        shell.StartInfo.UseShellExecute = false; // Required for redirection
        shell.StartInfo.CreateNoWindow = true; // Don't show a window
        
        try
        {
            shell.Start();

            // Read the output
            string output = shell.StandardOutput.ReadToEnd();
            string error = shell.StandardError.ReadToEnd();

            shell.WaitForExit(); // Wait for the process to finish

            // Display the results
            Console.WriteLine("Output: " + output);

            if (!string.IsNullOrEmpty(output))
            {
                return output;
            }
            else
            {
                return "Error: " + error;
            }
            
        }
        catch (Exception ex)
        {
            return "An error occurred: " + ex.Message;
        }
    }
    
}