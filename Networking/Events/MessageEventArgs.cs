using System;
using SmartReader.Message;
namespace SmartReader.Networking.Events
{
    public class MessageEventArgs : EventArgs
    {
        public IMessage Message { get; }
        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }
    }
}
