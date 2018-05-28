using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class GetBookMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.GetBook;
        public string Token { get; set; }
        public int BookId { get; set; }
    }
}
