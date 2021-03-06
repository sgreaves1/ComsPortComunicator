﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using ComsPortComunicator.Command;
using ComsPortComunicator.Data;
using ComsPortComunicator.Enum;
using ComsPortComunicator.Model;

namespace ComsPortComunicator.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPortNames = new ObservableCollection<string>();
        private ComPortModel _comPort = new ComPortModel();

        private ObservableCollection<ByteArrayModel> _byteArrayModels = new ObservableCollection<ByteArrayModel>();
        private ByteArrayModel _byteArrayModel; 

        private List<string> _comBaudRates = new List<string>();
        private List<string> _comDataBits = new List<string>();
        private List<string> _comStopBits = new List<string>();
        private List<string> _comParities = new List<string>();
        private List<string> _comHandShakes = new List<string>();

        
        private string _portOpenString;
        private DataToSendType _dataToSendType;
        private string _textToSend;
        private string _bytesAsString;
        private string _recievedText;

        public MainWindowViewModel()
        {
            PortOpenString = "Open Port";
            DataToSendType = DataToSendType.Strings;

            InitCommands();

            PopulateComPortLists();

            ComPort.PortDataReceivedEvent += DataRecieved;

            ReadData();
        }

        private void DataRecieved(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            byte[] bytes = ComPort.Read();

            foreach (var b in bytes)
            {
                if (b != 0)
                    RecievedText += b + " ";
            }

            RecievedText += "\n";
        }
        
        private void PopulateComPortLists()
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

        private void ReadData()
        {
            DataReader reader = new DataReader();

            ByteArrayModels = reader.GetByteArrayModels();
        }

        public void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            ComPort.Close();
        }

        private void PopulateByteText()
        {
            BytesAsString = "";

            if (ByteArrayModel != null)
            {
                foreach (byte b in ByteArrayModel.Bytes)
                {
                    BytesAsString += b;
                    BytesAsString += " ";
                }
            }
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

        public ObservableCollection<ByteArrayModel> ByteArrayModels
        {
            get { return _byteArrayModels; }
            set
            {
                _byteArrayModels = value;
                OnPropertyChanged();
            }
        }

        public ByteArrayModel ByteArrayModel
        {
            get { return _byteArrayModel; }
            set
            {
                _byteArrayModel = value;
                PopulateByteText();
                OnPropertyChanged();
            }
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

        
        
        public string PortOpenString
        {
            get { return _portOpenString; }
            set
            {
                _portOpenString = value;
                OnPropertyChanged();
            }
        }

        public DataToSendType DataToSendType
        {
            get {  return _dataToSendType; }
            set
            {
                _dataToSendType = value;
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

        public string BytesAsString
        {
            get { return _bytesAsString; }
            set
            {
                _bytesAsString = value;
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
            PortOpenString = "Open Port";

            ComPortNames.Clear();
            ComPort.PortName = "";

            RecievedText = "";

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
                PortOpenString = "Open Port";

            if (ComPort.State == ComOpenState.Open)
                PortOpenString = "Close Port";
        }

        private bool CanExecuteSendCommand()
        {
            switch (DataToSendType)
            {
                case DataToSendType.Bytes:
                    if (ByteArrayModel != null)
                        return true;

                    return false;
            }
            
            return true;
        }

        private void ExecuteSendCommand()
        {
            switch (DataToSendType)
            {
                case DataToSendType.Strings:
                    ComPort.Send(TextToSend);
                    TextToSend = "";
                break;
                case DataToSendType.Bytes:
                    ComPort.Send(ByteArrayModel.Bytes.ToArray());
                    ByteArrayModel = null;
                break;
            }
            
        }
    }
}
