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
            return new AuthenticationMessage(login, password);
        }

        public static IMessage MakeStatusMessage(Status status, string text)
        {
            return new StatusMessage(status, text);
        }

        public static IMessage MakeAuthenticateTokenMessage(string token)
        {
            return new AuthenticateTokenMessage(token);
        }

        public static IMessage MakeRegistrationMessage(string login, string password, string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
