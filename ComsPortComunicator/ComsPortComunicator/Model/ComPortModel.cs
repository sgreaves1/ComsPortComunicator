namespace ComsPortComunicator.Model
{
    public class ComPortModel : BaseModel
    {
        private string _portName;

        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                OnPropertyChanged();
            }
        }
    }
}
