using System;
using System.Collections.Generic;
using SmartReader.Message;
using SmartReader.Networking;
using SmartReader.Networking.Events;
using SmartReader.Database;

namespace SmartReader.Server
{
    // TODO REMOVE PUBLIC
    public static partial class Program
    {
        public static DatabaseConnection Conn;
        private static List<IConnection> Connections { get; }
        static Program()
        {
            Connections = new List<IConnection>();
        }
        static void Main(string[] args)
        {
            IConnectionListener listener = NetworkingFactory.CreateListener(8080);
            listener.ConnectionEstablished += OnIncomingConnection;
            Conn = new DatabaseConnection("root", "");

            listener.Start();

            Console.WriteLine("Server started. Press Enter to stop it.");
            Console.Read();
        }

        private static void OnIncomingConnection(object sender, ConnectionEventArgs e)
        {
            IConnection connection = e.Connection;
            connection.MessageReceived += OnIncomingMessage;
            connection.MessageReceived += OnIncomingMessageLogger;
            connection.Closed += OnConnectionClosed;
            Connections.Add(connection);
            connection.Open();
        }

        private static void OnConnectionClosed(object sender, EventArgs e)
        {
            Connections.Remove(sender as IConnection);
        }

        /// <summary>
        /// Обработка соединений. Самая объёмная часть, потому что типов соединений много.
        /// Необходимо исключить логику из этого метода. Здесь - только распределение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnIncomingMessage(object sender, MessageEventArgs e)
        {
            IMessage message = e.Message as IMessage;
            switch (message.Type)
            {
                case MessageTypes.Authenticate:
                    HandleAuthentication(message, sender as IConnection);
                    break;
                case MessageTypes.Registration:
                    HandleRegistration(message, sender as IConnection);
                    break;
                case MessageTypes.UploadBook:
                    HandleUploadBook(message, sender as IConnection);
                    break;
                case MessageTypes.GetBookList:
                    HandleGetBookList(message, sender as IConnection);
                    break;
                case MessageTypes.GetBook:
                    HandleGetBook(message, sender as IConnection);
                    break;
                case MessageTypes.DeleteBook:
                    HandleDeleteBook(message, sender as IConnection);
                    break;
            }
        }
        private static void OnIncomingMessageLogger(object sender, MessageEventArgs e)
        {
            
            Console.WriteLine("Пришло сообщение: тип=" + (int)e.Message.Type);

        }
    }
}