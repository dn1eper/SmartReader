using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class RegistrationMessage : IMessage
    {
        public MessageTypes Type => MessageTypes.Registration;
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
