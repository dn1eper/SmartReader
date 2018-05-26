using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    public class AuthenticateTokenMessage : IMessage
    {
        public DateTime CreatedAt => throw new NotImplementedException();

        public MessageTypes Type => MessageTypes.AuthenticateToken;

        public string Token;

        public AuthenticateTokenMessage(string token)
        {
            Token = token;
        }
    }
}
