using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Networking.Events;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SmartReader.Networking.Implementations
{
    /// <summary>
    /// Использует SocketConnection для всех входящих соединений.
    /// При установки соединения вызывается событие ConnectionEstablished.
    /// </summary>
    class SocketConnectionListener : IConnectionListener
    {
        private TcpListener listener; 
        public event ConnectionEventHandler ConnectionEstablished;

        public SocketConnectionListener(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }
        public void Stop()
        {
            listener.Stop();
        }

        public void Start()
        {
            listener.Start();
            Thread thread = new Thread(ThreadProc);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ThreadProc()
        {
            try
            {
                while (true)
                {
                    Socket socket = listener.AcceptSocket();
                    IConnection connection = new SocketConnection(socket);
                    OnConnectionEstablished(new ConnectionEventArgs(connection));
                }
            }
            catch (SocketException)
            {
                Stop();
            }
        }

        protected void OnConnectionEstablished(ConnectionEventArgs e)
        {
            ConnectionEstablished?.Invoke(this, e);
        }
           
    }
}
