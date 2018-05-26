using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Message;
using SmartReader.Message.Implementations;
using SmartReader.Networking;

namespace Server
{
    class MessageHandler
    {
        public static void HandleAuthentication(IMessage message, IConnection connection)
        {
            AuthenticationMessage authMessage = message as AuthenticationMessage;
            // TODO Запрос базе с логином и паролем
            // TODO в случае удачи вернуть сообщение
        }
    }
}
