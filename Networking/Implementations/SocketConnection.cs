using System;
using System.Net.Sockets;
using SmartReader.Message;
using SmartReader.Networking.Events;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SmartReader.Networking.Implementations
{
    /// <summary>
    /// Используется для отправки и приёма сообщения.
    /// Вероятно будет использоваться как на клиенте, так и на сервере.
    /// Для обработки входящих сообщений необходимо использовать событие MessageReceived.
    /// </summary>
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
            Thread thread = new Thread(ThreadProc);
            thread.IsBackground = true;
            thread.Start();

        }
        public void Close()
        {
            OnClosed(new EventArgs());
            Dispose();
        }

        /// <summary>
        /// Так как в качестве слушателя используется tcpconnection,
        /// то в одном tcp-сегменте могут прийти несколько сообщений.
        /// Поэтому для удобства выборки сообщений инкапсулируем их в 
        /// коллекцию, для каждого сообщения которой вызываем обработчик.
        /// 
        /// Таким образом достигается независимость от времени и количества
        /// полученных за раз сообщений.
        /// </summary>
        private void ThreadProc()
        {
            try
            {
                while (true)
                {
                    IEnumerable<IMessage> messages = Receive();
                    foreach (IMessage message in messages)
                    {
                        OnMessageReceived(new MessageEventArgs(message));
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
            IEnumerable<IMessage> messages;
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] buffer = new byte[1024];
                do
                {
                    int count = socket.Receive(buffer, 1024, SocketFlags.None);
                    stream.Write(buffer, 0, count);
                } while (stream.Length == 0 || socket.Available > 0);
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = stream.ToArray();
                messages = Utilities.Serialization.Deserialize(bytes);
            }
            return messages;
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

        protected void OnMessageReceived(MessageEventArgs e)
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
