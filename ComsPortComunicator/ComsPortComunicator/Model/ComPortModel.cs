using System;
using System.IO.Ports;
using ComsPortComunicator.Enum;

namespace ComsPortComunicator.Model
{
    public class ComPortModel : BaseModel
    {
        private string _portName;
        private string _baudRate;
        private string _dataBits;
        private string _stopBits;
        private string _parity;
        private string _handShake;
        private ComOpenState _state;
        private SerialPort _serialPort;

        public ComPortModel()
        {
            State = ComOpenState.Closed;
        }

        public void OpenClose()
        {
            switch (State)
            {
                    case ComOpenState.Open:
                    Close();
                    break;

                    case ComOpenState.Closed:
                    Open();
                    break;
            }
        }

        public void Open()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = PortName;
            _serialPort.BaudRate = Convert.ToInt32(BaudRate);
            _serialPort.DataBits = Convert.ToInt16(DataBits);
            _serialPort.StopBits = (StopBits)System.Enum.Parse(typeof(StopBits), StopBits);
            _serialPort.Handshake = (Handshake)System.Enum.Parse(typeof(Handshake), HandShake);
            _serialPort.Parity = (Parity)System.Enum.Parse(typeof(Parity), Parity);
            _serialPort.Open();
            State = ComOpenState.Open;
        }

        public void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                State = ComOpenState.Closed;
            }
        }

        public void Send(string text)
        {
            _serialPort.Write(text);
        }

        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                OnPropertyChanged();
            }
        }

        public string BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value;
                OnPropertyChanged();
            }
        }

        public string DataBits
        {
            get { return _dataBits; }
            set
            {
                _dataBits = value;
                OnPropertyChanged();
            }
        }

        public string StopBits
        {
            get { return _stopBits; }
            set
            {
                _stopBits = value;
                OnPropertyChanged();
            }
        }

        public string Parity
        {
            get { return _parity; }
            set
            {
                _parity = value;
                OnPropertyChanged();
            }
        }

        public string HandShake
        {
            get { return _handShake; }
            set
            {
                _handShake = value;
                OnPropertyChanged();
            }
        }

        public ComOpenState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }
    }
}
