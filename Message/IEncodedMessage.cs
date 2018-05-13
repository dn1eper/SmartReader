using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    interface IEncodedMessage
    {
        byte[] Encode(MessageTypes type);
        void Add(string paramName, string paramValue);
    }
}
