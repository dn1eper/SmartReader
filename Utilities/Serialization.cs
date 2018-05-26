using System;
using System.Xml.Serialization;
using System.Reflection; // extension of app domain
using SmartReader.Message;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// Просто отделяем логику сериализации от Network, потому что это довольно
    /// тонкое место, в котором могут быть баги.
    /// 
    /// Сериализуем и десериализуем с помощью вспомогательного класса.
    /// При десериализации возвращается коллекция сообщений, это обусловлено тем,
    /// что при пересылке сообщения могут склеиться в один TCP пакет.
    /// </summary>
    public static class Serialization
    {
        #region Helper classes
        private class Serializer
        {
            private XmlSerializer serializer;
            public Serializer()
            {
                Type[] types = AppDomain.CurrentDomain.GetTypes(IsSupportedMessageType);
                serializer = new XmlSerializer(types[0], types);
            }

            public IEnumerable<IMessage> Deserialize(MemoryStream stream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = stream.ToArray();
                
                string text = Encoding.Default.GetString(bytes, 0, bytes.Length);
                string[] splitted = text.Split(new string[] { "<?xml" }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string message in splitted)
                {
                    using (Stream s = ("<?xml" + message).ToStream())
                    {
                        yield return (IMessage) serializer.Deserialize(s);
                    }
                }
            }
            public byte[] Serialize(IMessage message)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, message);
                    stream.WriteByte(0); // ACHTUNG! разделитель
                    return stream.ToArray();
                }
            }

            private bool IsSupportedMessageType(Type type)
            {
                return type.GetCustomAttribute(typeof(SerializableAttribute)) != null
                        && typeof(IMessage).IsAssignableFrom(type);
            }
        }
        #endregion

        #region Serialization class code
        private static Serializer serializer = new Serializer();

        public static byte[] Serialize(IMessage message)
        {
            return serializer.Serialize(message);
        }

        public static IEnumerable<IMessage> Deserialize(Stream stream)
        {
            return serializer.Deserialize(stream as MemoryStream);
        }
        #endregion

    }
}
