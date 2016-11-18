using System;
using System.IO.Ports;
using System.Linq;
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
        private bool _openPortFailed = false;

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
            try
            {
                OpenPortFailed = false;
                _serialPort = new SerialPort();

                // Events
                _serialPort.DataReceived += PortDataReceivedEvent;
                _serialPort.ErrorReceived += SerialPortOnErrorReceived;
                _serialPort.PinChanged += SerialPortOnPinChanged;
                _serialPort.Disposed += SerialPortOnDisposed;

                _serialPort.PortName = PortName;
                _serialPort.BaudRate = Convert.ToInt32(BaudRate);
                _serialPort.DataBits = Convert.ToInt16(DataBits);
                _serialPort.StopBits = (StopBits)System.Enum.Parse(typeof(StopBits), StopBits);
                _serialPort.Handshake = (Handshake)System.Enum.Parse(typeof(Handshake), HandShake);
                _serialPort.Parity = (Parity)System.Enum.Parse(typeof(Parity), Parity);
                _serialPort.Open();
            }
            catch
            {
                _serialPort = null;
                OpenPortFailed = true;
                return;
            }
            
            State = ComOpenState.Open;
        }

        private void SerialPortOnDisposed(object sender, EventArgs eventArgs)
        {
            UpdateState();
        }

        private void SerialPortOnPinChanged(object sender, SerialPinChangedEventArgs serialPinChangedEventArgs)
        {
            UpdateState();
        }

        private void SerialPortOnErrorReceived(object sender, SerialErrorReceivedEventArgs serialErrorReceivedEventArgs)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    State = ComOpenState.Open;
                }
                else
                {
                    State = ComOpenState.Closed;
                }
                return;
            }

            State = ComOpenState.Closed;
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
            if (!string.IsNullOrEmpty(text))
                _serialPort.Write(text);
        }

        public void Send(byte[] bytes)
        {
            if (bytes != null)
                _serialPort.Write(bytes, 0, bytes.Count());
        }

        public byte[] Read()
        {
            byte[] bytes = new byte[100];
            _serialPort.Read(bytes, 0, bytes.Length);

            return bytes;
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

        public bool OpenPortFailed
        {
            get { return _openPortFailed; }
            set
            {
                _openPortFailed = value;
                OnPropertyChanged();
            }
        }

        public SerialDataReceivedEventHandler PortDataReceivedEvent;
    }
}
