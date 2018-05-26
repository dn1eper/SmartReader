using SmartReader.Message.Implementations;

namespace SmartReader.Message
{
    /// <summary>
    /// Все сообщения создаются фабрикой.
    /// </summary>
    public static class MessageFactory
    {
        public static IMessage MakeAuthenticateMessage(string login, string password)
        {
            return new AuthenticationMessage(login, password);
        }
    }
}
