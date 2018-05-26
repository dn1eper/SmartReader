using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Networking.Events
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);
    public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);
}
