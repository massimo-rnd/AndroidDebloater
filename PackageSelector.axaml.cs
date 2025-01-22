using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using AndroidDebloater.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AndroidDebloater;

public partial class PackageSelector : Window
{
    private ObservableCollection<AndroidPackage> _items;

        public PackageSelector()
        {
            InitializeComponent();
            
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
            
            LogBox.Text = "Uninstalling " + selectedItems.Count + " packages... \n";
            
            foreach (var item in selectedItems)
            {
                LogBox.Text += item + ": " +ShellExecutor.RemovePackage(item);
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

    public class AndroidPackage : INotifyPropertyChanged
    {
        private string _text;
        private bool _isChecked;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
            }
        }
        
        
    }
