using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    public class GetBookListMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.GetBookList;

        // Book list ? как выглядит ?
    }
}
