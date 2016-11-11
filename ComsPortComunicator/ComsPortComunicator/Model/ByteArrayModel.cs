using System.Collections.ObjectModel;

namespace ComsPortComunicator.Model
{
    public class ByteArrayModel : BaseModel
    {
        private string _name;

        private ObservableCollection<byte> _bytes= new ObservableCollection<byte>(); 

        public ByteArrayModel(string name, ObservableCollection<byte> bytes)
        {
            Name = name;
            Bytes = bytes;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<byte> Bytes
        {
            get { return _bytes; }
            set
            {
                _bytes = value;
                OnPropertyChanged();
            }
        } 
    }
}
