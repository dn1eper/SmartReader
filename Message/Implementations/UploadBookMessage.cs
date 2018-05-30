using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class UploadBookMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.UploadBook;
        public byte[] Content { get; set; }
        public string Title { get; set; }
        public string Token { get; set; }
    }
}
