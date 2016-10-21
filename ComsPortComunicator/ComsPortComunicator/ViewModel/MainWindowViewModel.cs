using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Windows.Input;
using ComsPortComunicator.Command;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPortNames = new ObservableCollection<string>(); 

        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ObservableCollection<string> ComPortNames
        {
            get { return _comPortNames; }
            set { _comPortNames = value; }
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
            string[] comNames = SerialPort.GetPortNames();

            foreach (string name in comNames)
            {
                ComPortNames.Add(name);
            }
        }
    }
}
