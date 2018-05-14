using SmartReader.Message.Implementation;

namespace SmartReader.Message
{
    public static class MessageFactory
    {
        public static IEncodedMessage MakeAuthenticateMessage(string login, string password)
        {
            return new SimpleEncodedMessage()
                .Add("login", login)
                .Add("password", password);
        }
    }
}
