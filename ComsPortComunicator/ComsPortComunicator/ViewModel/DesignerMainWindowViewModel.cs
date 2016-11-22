using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ComsPortComunicator.Enum;
using ComsPortComunicator.Model;

namespace ComsPortComunicator.ViewModel
{
    public class DesignerMainWindowViewModel
    {
        public ObservableCollection<string> ComPortNames => new ObservableCollection<string>() {"Com7", "Com8"};

        public ComPortModel ComPort => new ComPortModel() {PortName = "Com7", BaudRate = "300", HandShake = "None", DataBits = "5", StopBits = "One", Parity = "None", State = ComOpenState.Open};

        public List<string> ComBaudRates => new List<string>();

        public string PortOpenString => "Open";

        public DataToSendType DataToSendType => DataToSendType.Bytes;

        public ByteArrayModel ByteArrayModel => new ByteArrayModel("Fast Format Byte Array", new ObservableCollection<byte>() {25,98,34});

        public string BytesAsString
        {
            get
            {
                return ByteArrayModel.Bytes.Aggregate("", (current, b) => current + (b + " "));
            }
        }
    }
}
