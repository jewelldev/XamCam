using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamCam.ViewModels
{
    public class SimpleTabbedViewModel : INotifyPropertyChanged
    {
        public ICommand SelectItemCommand { get => new Command<string>((param) => PositionSelected = int.Parse(param)); }

        public int _positionSelected = 0;

        public string _sampleText = "this is sample";

        public string SampleText
        {
            get => _sampleText;
        }

        public int PositionSelected
        {
            set
            {
                if (_positionSelected != value)
                {
                    _positionSelected = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PositionSelected)));
                }
            }

            get => _positionSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
