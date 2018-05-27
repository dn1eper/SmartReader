using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Message;
using SmartReader.Message.Implementations;
using SmartReader.Networking;
using SmartReader.Database;

namespace SmartReader.Server
{
    // TODO REMOVE PUBLIC
    public static partial class Program
    {

        public static void HandleAuthentication(IMessage message, IConnection connection)
        {
            AuthenticationMessage authMessage = message as AuthenticationMessage;
        }
    }
}
