using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    public class AuthenticateResponseMessage : IMessage
    {
        public DateTime CreatedAt => throw new NotImplementedException();

        public MessageTypes Type => MessageTypes.AuthenticateResponse;

        public string Token;

        public AuthenticateResponseMessage(string token)
        {
            Token = token;
        }
    }
}
