using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Networking.Events;

namespace SmartReader.Networking.Implementations
{
    class SocketConnectionListener : IConnectionListener
    {
        public event ConnectionEventHandler ConnectionEstablished;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
