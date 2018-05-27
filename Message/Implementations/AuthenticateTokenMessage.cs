using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticateTokenMessage : IMessage
    {

        public MessageTypes Type => MessageTypes.AuthenticateToken;

        public string Token { get; set; }

    }
}
