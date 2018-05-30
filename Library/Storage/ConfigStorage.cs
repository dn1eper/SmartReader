using System;
using System.Collections.Generic;
using System.IO;

namespace SmartReader.Library.Storage
{
    // Config.Type = "Token" | "Username"

    public class ConfigStorage : XmlStorage
    {
        /// <summary>
        /// Параметры приложения
        /// </summary>
        public List<ConfigRecord> Configs { get; private set; }

        /// <summary>
        /// Локальное хранилище параметров приложения
        /// </summary>
        public ConfigStorage() :
            base(@"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader",
                "config.xml",
                typeof(List<ConfigRecord>))
        {
            Load();
        }

        /// <summary>
        /// Загружает параметры приложения с диска в буфер
        /// </summary>
        public override void Load()
        {
            if (file.Exists)
            {
                using (Stream stream = file.OpenRead())
                {
                    Configs = serializer.Deserialize(stream) as List<ConfigRecord>;
                }
            }
            else Configs = new List<ConfigRecord>();
        }

        /// <summary>
        /// Возвращает значение заданного параметра
        /// </summary>
        /// <param name="name">Имя параметра</param>
        public string GetValue(string name)
        {
            ConfigRecord config = new ConfigRecord()
            {
                Name = name,
                Value = ""
            };
            if (Configs.Contains(config)) return Configs[Configs.IndexOf(config)].Value;
            else return "";
        }

        /// <summary>
        /// Устанавливает значение заданного параметра
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        public void SetValue(string name, string value)
        {
            ConfigRecord config = new ConfigRecord()
            {
                Name = name,
                Value = value
            };
            if (Configs.Contains(config))
            {
                Configs.Remove(config);
            }
            Configs.Add(config);
        }

        /// <summary>
        /// Сохраняет на диск из буфера информацию о всех параметрах
        /// </summary>
        public override void Save()
        {
            file.Delete();
            using (Stream stream = file.OpenWrite())
            {
                serializer.Serialize(stream, Configs);
            }
        }
    }

    [Serializable]
    public class ConfigRecord
    {
        public string Name;
        public string Value;

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is ConfigRecord &&
                    obj.GetHashCode() == GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
