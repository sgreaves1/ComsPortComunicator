using System.Windows.Input;
using ComsPortComunicator.Command;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel
    {
        public ICommand RefreshPortsCommand { get; set; }

        public MainWindowViewModel()
        {
            InitCommands();
        }

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
            
        }
    }
}
