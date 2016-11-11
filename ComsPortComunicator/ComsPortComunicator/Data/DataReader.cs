using System;
using System.Collections.ObjectModel;
using ComsPortComunicator.Model;

namespace ComsPortComunicator.Data
{
    public class DataReader
    {
        private ObservableCollection<ByteArrayModel> _modelList = new ObservableCollection<ByteArrayModel>(); 

        public DataReader()
        {
            string text = System.IO.File.ReadAllText(@"Data/Data.txt");

            string[] lines = text.Split(new string[] { "<ByteArray>", "" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (line != "")
                {
                    string[] words = line.Split(new string[] {"<Name>", "<Bytes>", ""}, StringSplitOptions.None);

                    _modelList.Add(new ByteArrayModel(words[1], GetBytes(words[2])));
                }
            }

            int i = 0;
        }

        private ObservableCollection<Byte> GetBytes(string byteString)
        {
            ObservableCollection < Byte > result = new ObservableCollection<byte>();

            string[] eachByte = byteString.Split(new string[] {" ", ""}, StringSplitOptions.None);

            foreach (string s in eachByte)
            {
                if (s != "")
                {
                    byte b = (byte) int.Parse(s);

                    result.Add(b);
                }
            }

            return result;
        }

        public ObservableCollection<ByteArrayModel> GetByteArrayModels()
        {
            return _modelList;
        }
    }
}
