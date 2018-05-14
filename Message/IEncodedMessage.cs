using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message
{
    public interface IEncodedMessage
    {
        byte[] Encode(MessageTypes type);
        IEncodedMessage Add(string paramName, string paramValue);
    }
}
