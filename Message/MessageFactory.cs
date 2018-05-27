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
            // TODO hash passwords
            return new AuthenticationMessage() { Login = login, Password = password };
        }

        public static IMessage MakeStatusMessage(Status status, string text)
        {
            return new StatusMessage() { Status = status, Text = text };
        }

        public static IMessage MakeAuthenticateResponseMessage(string token)
        {
            return new AuthenticateResponseMessage() { Token = token };
        }
    }
}
