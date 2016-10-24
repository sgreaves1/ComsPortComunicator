﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using ComsPortComunicator.Command;
using ComsPortComunicator.Enum;
using ComsPortComunicator.Model;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPortNames = new ObservableCollection<string>();
        private List<string> _comBaudRates = new List<string>();
        private List<string> _comDataBits = new List<string>();
        private List<string> _comStopBits = new List<string>();
        private List<string> _comParities = new List<string>();
        private List<string> _comHandShakes = new List<string>();

        private ComPortModel _comPort = new ComPortModel();

        private string _portOpenString;

        private string _textToSend;

        private string _recievedText;
        

        public MainWindowViewModel()
        {
            PortOpenString = "Open";

            InitCommands();

            PopulateLists();

            ComPort.PortDataReceivedEvent += DataRecieved;
        }

        private void DataRecieved(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            RecievedText += ComPort.ReadExisting();
            RecievedText += "\n";
        }
        
        private void PopulateLists()
        {
            ComBaudRates.Add("300");
            ComBaudRates.Add("600");
            ComBaudRates.Add("1200");
            ComBaudRates.Add("2400");
            ComBaudRates.Add("9600");
            ComBaudRates.Add("14400");
            ComBaudRates.Add("19200");
            ComBaudRates.Add("38400");
            ComBaudRates.Add("57600");
            ComBaudRates.Add("115200");
            
            ComDataBits.Add("5");
            ComDataBits.Add("6");
            ComDataBits.Add("7");
            ComDataBits.Add("8");
            
            ComStopBits.Add("One");
            ComStopBits.Add("OnePointFive");
            ComStopBits.Add("Two");

            ComParities.Add("None");
            ComParities.Add("Even");
            ComParities.Add("Mark");
            ComParities.Add("Odd");
            ComParities.Add("Space");

            ComHandShakes.Add("None");
            ComHandShakes.Add("XOnXOff");
            ComHandShakes.Add("RequestToSend");
            ComHandShakes.Add("RequestToSendXOnxOff");
        }

        public void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            ComPort.Close();
        }

        public ObservableCollection<string> ComPortNames
        {
            get { return _comPortNames; }
            set { _comPortNames = value; }
        }

        public List<string> ComBaudRates
        {
            get { return _comBaudRates; }
            set { _comBaudRates = value; }
        }

        public List<string> ComDataBits
        {
            get { return _comDataBits; }
            set { _comDataBits = value; }
        }

        public List<string> ComStopBits
        {
            get { return _comStopBits; }
            set { _comStopBits = value; }
        }

        public List<string> ComParities
        {
            get { return _comParities; }
            set { _comParities = value; }
        }

        public List<string> ComHandShakes
        {
            get { return _comHandShakes; }
            set { _comHandShakes = value; }
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
        
        public string PortOpenString
        {
            get { return _portOpenString; }
            set
            {
                _portOpenString = value;
                OnPropertyChanged();
            }
        }

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                OnPropertyChanged();
            }
        }

        public string RecievedText
        {
            get { return _recievedText; }
            set
            {
                _recievedText = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshPortsCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SendCommand { get; set; }

        private void InitCommands()
        {
            RefreshPortsCommand = new RelayCommand(ExecuteRefreshPortsCommand, CanExecuteRefreshPortsCommand);
            OpenCommand = new RelayCommand(ExecuteOpenCommand, CanExecuteOpenCommand);
            SendCommand = new RelayCommand(ExecuteSendCommand, CanExecuteSendCommand);
        }

        private bool CanExecuteRefreshPortsCommand()
        {
            return true;
        }

        private void ExecuteRefreshPortsCommand()
        {
            ComPort.Close();
            PortOpenString = "Open";

            ComPortNames.Clear();
            ComPort.PortName = "";

            string[] comNames = SerialPort.GetPortNames();

            foreach (string name in comNames)
            {
                ComPortNames.Add(name);
            }

            if (ComPortNames.Count > 0)
            {
                ComPort.PortName = ComPortNames.First();
                ComPort.BaudRate = ComBaudRates.First();
                ComPort.DataBits = ComDataBits.First();
                ComPort.HandShake = ComHandShakes.First();
                ComPort.Parity = ComParities.First();
                ComPort.StopBits = ComStopBits.First();
            }
        }

        private bool CanExecuteOpenCommand()
        {
            return true;
        }

        private void ExecuteOpenCommand()
        {
            ComPort.OpenClose();

            if (ComPort.State == ComOpenState.Closed)
                PortOpenString = "Open";

            if (ComPort.State == ComOpenState.Open)
                PortOpenString = "Close";
        }

        private bool CanExecuteSendCommand()
        {
            return true;
        }

        private void ExecuteSendCommand()
        {
            ComPort.Send(TextToSend);
            TextToSend = "";
        }
    }
}
