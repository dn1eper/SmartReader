using System;
using System.Collections.Generic;
using SmartReader.Message;
using SmartReader.Networking;
using SmartReader.Networking.Events;
namespace Server
{
    static class Program
    {
        private static List<IConnection> Connections { get; }
        static Program()
        {
            Connections = new List<IConnection>();
        }
        static void Main(string[] args)
        {
            IConnectionListener listener = NetworkingFactory.CreateListener(8080);
            listener.ConnectionEstablished += OnIncomingConnection;
            listener.Open();
            Console.WriteLine("Server started. Press Enter to stop it.");
            Console.Read();
            // TODO закрывать соединение с базой
            // TODO закрывать TCP-соединения
        }

        private static void OnIncomingConnection(object sender, ConnectionEventArgs e)
        {
            IConnection connection = e.Connection;
            connection.MessageReceived += OnIncomingMessage;
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
                    MessageHandler.HandleAuthentication(message, sender as IConnection);
                    break;
            }
        }
    }
}