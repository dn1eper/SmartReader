using System;
using SmartReader.Message;
using SmartReader.Message.Implementations;
using SmartReader.Networking.Events;
using SmartReader.Networking;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {

            using (IConnection connection = NetworkingFactory.OpenConnection("localhost", 8080))
            {
                connection.Open();
                IMessage msg = MessageFactory.MakeAuthenticateMessage("admin", "password2");
                connection.Send(msg);
                connection.MessageReceived += OnMessage;
                Console.Read();
                connection.Close();
            }
        }

        static void OnMessage(object sender, MessageEventArgs args)
        {
            Console.WriteLine("Ответ получен: тип=" + (int)args.Message.Type);
        }
    }
}
