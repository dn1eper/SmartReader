﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message
{
    public interface IDecodedMessage
    {
        MessageTypes DecodeMessageType();
        // TODO Добавить извлечение данных всех возможных типов MessageType
    }
}
