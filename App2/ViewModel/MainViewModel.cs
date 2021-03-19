using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App2.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _number;
        private int _max;
        private readonly Random _random;
        public RelayCommand Roll { get; set; }
        public ParametrizedRelayCommand SetMax { get; set; }

        public MainViewModel()
        {
            Roll = new RelayCommand(
                () =>
                {
                    Number = _random.Next(Max);
                },
                () => { return Number < 5; }
            );
            SetMax = new ParametrizedRelayCommand(
                (parameter) =>
                {
                    if (Int32.TryParse(parameter as string, out int result))
                        Max = result;
                },
                () => { return true; }
            );
            Max = 6;
            _random = new Random();
            Number = _random.Next(Max);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Number { get { return _number; } set { _number = value; NotifyPropertyChanged(); Roll.RaiseCanExecuteChanged(); } }

        public int Max { get { return _max; } set { _max = value; NotifyPropertyChanged(); } }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
