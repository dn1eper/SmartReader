using System;
using System.Net.Sockets;
using SmartReader.Message;
using SmartReader.Networking.Events;
using System.Collections.Generic;
using System.IO;

namespace SmartReader.Networking.Implementations
{
    class SocketConnection : IConnection
    {
        Socket socket;

        public event MessageEventHandler MessageReceived;
        public event EventHandler Closed;

        internal SocketConnection(Socket socket)
        {
            this.socket = socket;
        }
        public void Open()
        {
            throw new NotImplementedException();
        }
        public void Close()
        {
            OnClosed(new EventArgs());
            Dispose();
        }

        private void ThreadProc()
        {
            try
            {
                while (true)
                {
                    IEnumerable<IMessage> messages = Receive();
                    foreach (IMessage message in messages)
                    {
                        OnMessageRevieved(new MessageEventArgs(message));
                    }
                }
            }
            catch (SocketException)
            {
                Close();
            }
        }

        private IEnumerable<IMessage> Receive()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] buffer = new byte[1024];
                do
                {
                    int count = socket.Receive(buffer, 1024, SocketFlags.None);
                    stream.Write(buffer, 0, count);
                } while (stream.Length == 0 || socket.Available > 0);
                stream.Seek(0, SeekOrigin.Begin);
                return Utilities.Serialization.Deserialize(stream); // (sic!)
            }
        }

        public void Send(IMessage message)
        {
            socket.Send(Utilities.Serialization.Serialize(message));
        }

                #region EventsCalling
        protected void OnClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        protected void OnMessageRevieved(MessageEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }
        #endregion

        public void Dispose()
        {
            if (socket != null)
            {
                socket.Dispose();
            }
        }
    }
}
