using System;
using System.Collections.Generic;
using System.IO;

namespace SmartReader.Library.Storage
{
    public class ConfigStorage : XmlStorage
    {
        public List<ConfigRecord> Configs { get; private set; }

        public ConfigStorage() :
            base(@"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader",
                "config.xml",
                typeof(List<ConfigRecord>))
        {
            Load();
        }

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

        public string GetValue(string name)
        {
            ConfigRecord configRecord = new ConfigRecord()
            {
                Name = name,
                Value = ""
            };
            if (Configs.Contains(configRecord)) return Configs[Configs.IndexOf(configRecord)].Value;
            else return "";
        }

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
