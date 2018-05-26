using System;
using System.IO;
using System.Xml.Serialization;

namespace SmartReader.Library.Storage
{
    public abstract class XmlStorage
    {
        public string Path { get; private set; }
        public string FileName { get; private set; }

        protected FileInfo file;
        protected XmlSerializer serializer;

        public XmlStorage(string path, string fileName, Type type)
        {
            Path = path;
            FileName = fileName;
            // Создание директории для хранения файла, если ее нет
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            // Определяем путь файла лицензии
            file = new FileInfo(Path + @"\" + FileName);
            // Инициализируем сериализатор
            serializer = new XmlSerializer(type);
        }

        abstract public void Load();
        abstract public void Save();

        public void Close() => Save();
    }
}
