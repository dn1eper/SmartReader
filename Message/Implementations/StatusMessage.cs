using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    public class StatusMessage : IMessage
    {
        public DateTime CreatedAt => throw new NotImplementedException();

        public MessageTypes Type => MessageTypes.Status;

        public Status Status;
        public string Text;

        public StatusMessage(Status status, string text)
        {
            Status = status;
            Text = text;
        }
    }
}
