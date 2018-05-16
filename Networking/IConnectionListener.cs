using System;
using SmartReader.Networking.Events;

namespace SmartReader.Networking
{
    public interface IConnectionListener
    {
        event ConnectionEventHandler ConnectionEstablished;
        void Open();
        void Close();
    }
}
