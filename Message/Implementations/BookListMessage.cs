using System;
using System.Collections.Generic;
using SmartReader.Library.Book;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class BookListMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.BookList;
        public List<BookRecord> BookList;
    }
}
