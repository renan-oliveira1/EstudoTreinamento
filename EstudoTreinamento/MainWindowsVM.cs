using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoTreinamento
{
    public class MainWindowsVM: INotifyPropertyChanged{

        public RelayCommand show { get; private set; }
        public string stringBox { get; set; }
        public ObservableCollection<string> strings { get; set; }

        public MainWindowsVM() { 
            strings = new ObservableCollection<string>();
            InitializeCommands();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeCommands()
        {
            show = new RelayCommand((object _) =>
            {
                strings.Add(stringBox);
                Notify("stringBox");
            });
        }
    }

 
}
