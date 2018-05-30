using SmartReader.Library.Book;
using SmartReader.Message.Implementations;
using System.Collections.Generic;

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

        public static IMessage MakeRegistrationMessage(string login, string password, string email)
        {
            return new RegistrationMessage() { Login = login, Password = password };
        }

        public static IMessage MakeAuthenticationResposeMessage(string token, Status status=Status.Ok, string message="")
        {
            return new AuthenticationResponseMessage() { Status = status, Token = token, Message = message};
        }
        public static IMessage MakeUploadBookMessage(string title, string text, string token)
        {
            return new UploadBookMessage() { Title = title, Content = text, Token = token };
        }
        public static IMessage MakeGetBookListMessage(string token)
        {
            return new GetBookListMessage() { Token = token };
        }
        public static IMessage MakeBookListMessage(List<BookInfo> bookList)
        {
            return new BookListMessage() { Books = bookList };
        }

        public static IMessage MakeGetBookMessage(string token, int bookId)
        {
            return new GetBookMessage() { Token = token, BookId = bookId };
        }
        public static IMessage MakeBookMessage(int bookId, string content, string title)
        {
            return new BookMessage() { Content = content, BookId = bookId, Title = title };
        }

        public static IMessage MakeDeleteBookMessage(string token, int bookId)
        {
            return new DeleteBookMessage() { Token = token, BookId = bookId };
        }
    }
}
