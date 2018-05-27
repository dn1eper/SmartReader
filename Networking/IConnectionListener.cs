using System;
using SmartReader.Networking.Events;

namespace SmartReader.Networking
{
    public interface IConnectionListener
    {
        event ConnectionEventHandler ConnectionEstablished;
        void Start();
        void Stop();
    }
}
