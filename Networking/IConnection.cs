using System;
using SmartReader.Message;
using SmartReader.Networking.Events;

namespace SmartReader.Networking
{
    public interface IConnection : IDisposable
    {
        event MessageEventHandler MessageReceived;
        event EventHandler Closed;
        void Open();
        void Close();
        void Send(IMessage message);
    }
}
