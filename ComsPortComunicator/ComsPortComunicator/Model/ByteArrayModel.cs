namespace ComsPortComunicator.Model
{
    public class ByteArrayModel : BaseModel
    {
        private string _name;

        public ByteArrayModel(string name)
        {
            Name = name;
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
    }
}
