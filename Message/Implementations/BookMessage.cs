using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class BookMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.Book;
        public Int32 BookId { get; set; }
        public string Content { get; set; }
    }
}
