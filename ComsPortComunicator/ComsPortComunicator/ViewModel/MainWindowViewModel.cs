using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using ComsPortComunicator.Command;
using ComsPortComunicator.Model;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPortNames = new ObservableCollection<string>();
        private ComPortModel _comPort = new ComPortModel();

        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ObservableCollection<string> ComPortNames
        {
            get { return _comPortNames; }
            set { _comPortNames = value; }
        }

        public ComPortModel ComPort
        {
            get { return _comPort; }
            set
            {
                _comPort = value;
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
            ComPort.PortName = "";

            string[] comNames = SerialPort.GetPortNames();

            foreach (string name in comNames)
            {
                ComPortNames.Add(name);
            }

            if (ComPortNames.Count > 0)
                ComPort.PortName = ComPortNames.First();
        }
    }
}
