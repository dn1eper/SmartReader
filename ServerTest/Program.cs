using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Message;
using SmartReader.Networking;
using SmartReader.Networking.Events;
using SmartReader.Server;
using SmartReader.Database;

namespace ServerTest
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
            listener.Start();

            DatabaseConnection conn = new DatabaseConnection("root", "");
            Console.WriteLine("Connected");
            SmartReader.Server.Program.Conn = conn;

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
            Console.WriteLine("Соединение прервано с одним клиентом");
        }

        private static void OnIncomingMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Пришло сообщение: тип=" + (int)e.Message.Type);
            IMessage message = e.Message as IMessage;
            switch (message.Type)
            {
                case MessageTypes.Authenticate:
                    SmartReader.Server.Program.HandleAuthentication(message, sender as IConnection);
                    break;
            }
        }
    }
}
