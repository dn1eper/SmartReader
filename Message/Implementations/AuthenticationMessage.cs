using System;
using SmartReader.Message;
namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticationMessage : IMessage
    {
        private long createdAt;
        public DateTime CreatedAt
        {
            get => DateTime.FromBinary(createdAt);
            set => createdAt = value.ToBinary();
        }
        public MessageTypes Type { get => MessageTypes.Authenticate; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public AuthenticationMessage(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
