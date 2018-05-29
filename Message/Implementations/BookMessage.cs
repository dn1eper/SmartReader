using System;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class BookMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.Book;
        public Int32 BookId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
