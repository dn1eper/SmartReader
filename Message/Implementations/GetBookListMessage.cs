using System;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class GetBookListMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.GetBookList;
        public string Token;
    }
}
