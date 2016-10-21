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

    }
}
