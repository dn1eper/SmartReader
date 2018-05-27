using System;
using SmartReader.Message;
namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticationMessage : IMessage
    {
        public MessageTypes Type { get => MessageTypes.Authenticate; }
        public string Login { get;  set; }
        public string Password { get; set; }
      
    }
}
