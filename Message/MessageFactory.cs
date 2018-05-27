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

        public static IMessage MakeAuthenticateTokenMessage(string token)
        {
            return new AuthenticateTokenMessage(){Token = token};
        }

        public static IMessage MakeRegistrationMessage(string login, string password, string email)
        {
            return new RegistrationMessage() { Login = login, Password = password };
        }

        public static IMessage MakeAuthenticationResposeMessage(string token, Status status=Status.Ok, string message="")
        {
            return new AuthenticationResponseMessage() { Status = status, Token = token, Message = message};
        }
    }
}
