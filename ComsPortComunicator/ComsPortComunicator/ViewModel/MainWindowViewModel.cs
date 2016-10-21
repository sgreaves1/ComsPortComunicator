using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using ComsPortComunicator.Command;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPortNames = new ObservableCollection<string>();
        private string _selectedPortName;

        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ObservableCollection<string> ComPortNames
        {
            get { return _comPortNames; }
            set { _comPortNames = value; }
        }

        public string SelectedPortName
        {
            get { return _selectedPortName; }
            set
            {
                _selectedPortName = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshPortsCommand { get; set; }

        private void InitCommands()
        {
            RefreshPortsCommand = new RelayCommand(RefreshPortsCommandExecute, RefreshPortsCommandCanExecute);
        }

        private bool RefreshPortsCommandCanExecute()
        {
            return true;
        }

        private void RefreshPortsCommandExecute()
        {
            ComPortNames.Clear();
            SelectedPortName = "";

            string[] comNames = SerialPort.GetPortNames();

            foreach (string name in comNames)
            {
                ComPortNames.Add(name);
            }

            if (ComPortNames.Count > 0)
                SelectedPortName = ComPortNames.First();
        }
    }
}
