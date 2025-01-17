using System;
using System.Reflection;
using System.Text.RegularExpressions;
using AndroidDebloater.Components;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace AndroidDebloater
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DebloatBtn.IsEnabled = false;
        }

        public void ShowHelp(object sender, RoutedEventArgs args)
        {
            var helpBox = MessageBoxManager.GetMessageBoxStandard("Help", "Please Enable Developer-Mode on your Android Device by clicking the Build-Number 7 Times, then in Developer-Options enable USB Debugging.\nWhen plugging your device into your PC it should show a message about trusting the PC. Click allow.", MsBox.Avalonia.Enums.ButtonEnum.Ok);
            var result = helpBox.ShowAsPopupAsync(this);
        }

        public void ListDevices(object sender, RoutedEventArgs args)
        {
            clOutput.Text = ShellExecutor.ListADB();
            // Regular expression to match the exact word "device"
            string pattern = @"\bdevice\b";

            // Match only lines with the exact word "device"
            foreach (string line in clOutput.Text.Split('\n'))
            {
                if (Regex.IsMatch(line.Trim(), pattern))
                {
                    Console.WriteLine($"Matched: {line.Trim()}");
                    DebloatBtn.IsEnabled = true;
                }
            }
        }

        public void StartDebloater(object sender, RoutedEventArgs args)
        {
            int packageValue;
            
            if ((bool)gDebloat.IsChecked)
            {
                clOutput.Text = ShellExecutor.StartDebloat(1);
            }else if ((bool)aDebloat.IsChecked)
            {
                clOutput.Text = ShellExecutor.StartDebloat(2);
            }else if ((bool)tpDebloat.IsChecked)
            {
                clOutput.Text = ShellExecutor.StartDebloat(3);
            }
            else
            {
                clOutput.Text = ShellExecutor.StartDebloat(4);
            }
        }
    }
}