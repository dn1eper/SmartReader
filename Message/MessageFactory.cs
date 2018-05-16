using SmartReader.Message.Implementations;

namespace SmartReader.Message
{
    public static class MessageFactory
    {
        public static IMessage MakeAuthenticateMessage(string login, string password)
        {
            return new AuthenticationMessage(login, password);
        }
    }
}
