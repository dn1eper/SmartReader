using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticationResponseMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.AuthenticateResponse;
        public string Token { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
    }
}
