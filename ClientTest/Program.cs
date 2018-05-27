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
        private static string TOKEN;
        static void Main(string[] args)
        {

            using (IConnection connection = NetworkingFactory.OpenConnection("localhost", 8080))
            {
                connection.Open();
                connection.MessageReceived += OnMessage;
                IMessage msg;
                msg = MessageFactory.MakeRegistrationMessage("admin", "password", "mail@mail.ru");
                connection.Send(msg);
                msg = MessageFactory.MakeAuthenticateMessage("admin", "asd");
                connection.Send(msg);
                msg = MessageFactory.MakeAuthenticateMessage("admin", "password");
                connection.Send(msg);
                msg = MessageFactory.MakeAuthenticateMessage("admin1", "asd");
                connection.Send(msg);
                Console.ReadLine();
                msg = MessageFactory.MakeUploadBookMessage("Martin Iden", "Good book!", TOKEN);
                connection.Send(msg);
                Console.Read();
                connection.Close();
            }
        }

        static void OnMessage(object sender, MessageEventArgs args)
        {
            Console.WriteLine("Ответ получен: тип=" + (int)args.Message.Type);
            switch (args.Message.Type)
            {

                case MessageTypes.AuthenticateResponse:
                    Console.WriteLine("Status: " + (args.Message as AuthenticationResponseMessage).Status);
                    Console.WriteLine("Token: " + (args.Message as AuthenticationResponseMessage).Token);
                    Console.WriteLine("Message: " + (args.Message as AuthenticationResponseMessage).Message);
                    Console.WriteLine();
                    if (!(args.Message as AuthenticationResponseMessage).Token.IsEmpty())
                    {
                        TOKEN = (args.Message as AuthenticationResponseMessage).Token;
                        Console.WriteLine(TOKEN);
                    }
                    break;
                case MessageTypes.Status:
                    Console.WriteLine("Status: " + (args.Message as StatusMessage).Status);
                    Console.WriteLine("Message: " + (args.Message as StatusMessage).Text);
                    break;
            }
        }
    }
}
