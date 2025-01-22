using System.ComponentModel;

namespace AndroidDebloater.Components;

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