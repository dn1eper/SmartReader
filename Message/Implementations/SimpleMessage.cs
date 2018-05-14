using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementation
{
    class SimpleMessage
    {
        private Encoding encoding;
        private char paramSeparator; // Между названием и значением
        private char tokenSeparator; // Между параметрами
        private string message;
        public SimpleMessage()
        {
            encoding = new UTF8Encoding(true, true);
            paramSeparator = '\u0014';
            tokenSeparator = '\u0015';
        }

        public SimpleMessage(byte[] bytes) : this()
        {
            char[] chars = new char[bytes.Length];
            encoding.GetDecoder().GetChars(bytes, 0, bytes.Length, chars, 0);
            message = chars.ToString();
        }
        public byte[] Encode(MessageTypes type)
        {
            return encoding.GetBytes(Convert.ToString(type) + tokenSeparator + message);
        }
        /// <summary>
        /// Кодирует имена в строку, разделяя параметры с помощью непечатный символов.
        /// </summary>
        /// <param name="paramName">Название параметра</param>
        /// <param name="paramValue">Значение параметра</param>
        public void Add(string paramName, string paramValue)
        {
            // Итоговое сообщение будет всегда завершаться tokenSeparator
            message += paramName + paramSeparator + paramValue + tokenSeparator;
        }
        /// <summary>
        /// Берёт первый символ - код сообщения (число)
        /// </summary>
        /// <returns>Преобразованное к MessageType число</returns>
        public MessageTypes DecodeMessageType()
        {
            // TODO проверка на соответствие границам или выброс исключения
            return (MessageTypes) Int32.Parse(message.Split(tokenSeparator)[0]);
        }
    }
    /// <summary>
    /// Классы SimpleEncodedMessage и SimpleDecodedMessage реализуют лишь
    /// обёртки над имеющимся функционалом, но зато ограничивают использование
    /// согласно семантике формирования или получения сообщения.
    /// </summary>
    public class SimpleEncodedMessage : IEncodedMessage
    {
        private SimpleMessage message;
        public SimpleEncodedMessage()
        {
            message = new SimpleMessage();
        }
        public byte[] Encode(MessageTypes type)
        {
            return message.Encode(type);
        }
        public void Add(string paramName, string paramValue)
        {
            message.Add(paramName, paramValue);
        }
    }
    public class SimpleDecodedMessage : IDecodedMessage
    {
        private SimpleMessage message;
        public SimpleDecodedMessage(byte[] bytes)
        {
            message = new SimpleMessage(bytes);
        }

        public MessageTypes DecodeMessageType()
        {
            return message.DecodeMessageType();
        }
    }
}
