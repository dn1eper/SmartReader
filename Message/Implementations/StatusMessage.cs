using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class StatusMessage : IMessage
    {
        public DateTime CreatedAt => DateTime.Now;

        public MessageTypes Type => MessageTypes.Status;

        public Status Status { get; set; }
        public string Text { get; set; }

      
    }
}
