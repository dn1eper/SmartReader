using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticateResponseMessage : IMessage
    {
        public DateTime CreatedAt => DateTime.Now;

        public MessageTypes Type => MessageTypes.AuthenticateResponse;

        public string Token { get; set; }

    }
}
