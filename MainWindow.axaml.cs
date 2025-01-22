using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using AndroidDebloater.Components;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.TextFormatting.Unicode;
using MsBox.Avalonia;

namespace AndroidDebloater
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<AndroidPackage> _items;
        
        public MainWindow()
        {
            InitializeComponent();
            DebloatBtn.IsEnabled = false;
            CDebloatBtn.IsEnabled = false;
            mSelector.IsEnabled = false;
            sSelector.IsEnabled = false;
            ScriptPanel.IsVisible = false;
            CustomPanel.IsVisible = false;
            cSelector.IsEnabled = false;
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
                    CDebloatBtn.IsEnabled = true;
                    cSelector.IsEnabled = true;
                    sSelector.IsEnabled = true;
                    ScriptPanel.IsVisible = true;
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
                //Manufacturer Debloat
                int selectedIndex = mSelector.SelectedIndex;

                switch (selectedIndex)
                {
                    case 0:
                        //Google
                        clOutput.Text = ShellExecutor.StartDebloat(4);
                        break;
                    case 1:
                        //Huawei
                        clOutput.Text = ShellExecutor.StartDebloat(5);
                        break;
                    case 2:
                        //Oneplus
                        clOutput.Text = ShellExecutor.StartDebloat(6);
                        break;
                    case 3:
                        //Oppo
                        clOutput.Text = ShellExecutor.StartDebloat(7);
                        break;
                    case 4:
                        //Realme
                        clOutput.Text = ShellExecutor.StartDebloat(8);
                        break;
                    case 5:
                        //Samsung
                        clOutput.Text = ShellExecutor.StartDebloat(9);
                        break;
                    case 6:
                        //Vivo
                        clOutput.Text = ShellExecutor.StartDebloat(10);
                        break;
                    case 7:
                        //Xiaomi
                        clOutput.Text = ShellExecutor.StartDebloat(11);
                        break;
                }
            }
        }

        public void EnableSelector(object sender, RoutedEventArgs args)
        {
            mSelector.IsEnabled = true;
        }

        public void DisableSelector(object sender, RoutedEventArgs args)
        {
            mSelector.IsEnabled = false;
        }

        public void ShowScripts(object sender, RoutedEventArgs args)
        {
            ScriptPanel.IsVisible = true;
            CustomPanel.IsVisible = false;
        }

        public void ShowCustomSelector(object sender, RoutedEventArgs args)
        {
            CustomPanel.IsVisible = true;
            ScriptPanel.IsVisible = false;
            
            _items = new ObservableCollection<AndroidPackage>(CreateObservableCollection(ShellExecutor.GetPackages()));

            // Get the ItemsControl by name and set its ItemsSource
            var packageControl = this.FindControl<ItemsControl>("PackageList");
            packageControl.ItemsSource = _items;
        }
        
        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            
            var selectedItems = new List<string>();
            foreach (var item in _items)
            {
                if (item.IsChecked)
                {
                    selectedItems.Add(item.Text);
                }
            }
            
            clOutput.Text = "Uninstalling " + selectedItems.Count + " packages... \n";
            
            foreach (var item in selectedItems)
            {
                clOutput.Text += item + ": " +ShellExecutor.RemovePackage(item);
            }
        }
        
        public ObservableCollection<AndroidPackage> CreateObservableCollection(string input)
        {
            var collection = new ObservableCollection<AndroidPackage>();

            // Split the input into lines
            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                // Remove the "package:" prefix and add to the collection
                var cleanedLine = line.Replace("package:", "").Trim();
                collection.Add(new AndroidPackage { Text = cleanedLine, IsChecked = false });
            }

            return collection;
        }
    }
}