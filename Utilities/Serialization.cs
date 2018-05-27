using System;
using System.Xml.Serialization;
using System.Reflection; // extension of app domain
using SmartReader.Message;
using SmartReader.Message.Implementations;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartReader.Utilities
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
            private Dictionary<Type, XmlSerializer> serializers;
            public Serializer()
            {
                serializers = new Dictionary<Type, XmlSerializer>();
                Type[] types = AppDomain.CurrentDomain.GetTypes(IsSupportedMessageType);
                foreach (Type type in types)
                {
                    serializers[type] = new XmlSerializer(type);
                }
            }

            public IEnumerable<IMessage> Deserialize(byte[] bytes)
            {
                string text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                string[] splitted = text.Split(new string[] { "<?" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string message in splitted)
                {
                    yield return (IMessage)DeserializeString("<?" + message);
                }
            }

            private IMessage DeserializeString(string str)
            {
                bool success = false;
                IMessage message = null;
                Dictionary<Type, XmlSerializer>.KeyCollection keys = serializers.Keys;
                using (MemoryStream s = str.ToStream())
                {
                    foreach (Type t in keys)
                    {
                        try
                        {
                            message = (IMessage) serializers[t].Deserialize(s);
                            success = true;
                            break;
                        }
                        catch (Exception)
                        {
                            s.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }
                if (success) return message;
                throw new Exception("Not implemented type of deserialization");
                
            }

            public byte[] Serialize(IMessage message)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    SerializeOneOf(stream, message);
                    //stream.WriteByte(0); // ACHTUNG! разделитель
                    return stream.ToArray();
                }
            }

            private void SerializeOneOf(MemoryStream stream, IMessage message)
            {
                bool success = false;
                Dictionary<Type, XmlSerializer>.KeyCollection keys = serializers.Keys;
                foreach (Type t in keys)
                {
                    try
                    {
                        serializers[t].Serialize(stream, message);
                        success = true;
                        break;
                    }
                    catch (Exception)
                    {
                        //pass
                    }
                }
                if (!success) 
                    throw new Exception("Not implemented type of serialization");
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

        public static IEnumerable<IMessage> Deserialize(byte[] bytes)
        {
            return serializer.Deserialize(bytes);
        }
        #endregion

    }
}
