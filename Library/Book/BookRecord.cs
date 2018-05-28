using System;

namespace SmartReader.Library.Book
{
    [Serializable]
    public class BookRecord
    {
        public string Path { get; set; }
        public int Offset { get; set; }

        // TODO: Добавить хранение закладок

        public override int GetHashCode()
        {
            return System.IO.Path.GetFileNameWithoutExtension(Path).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is BookRecord &&
                    obj.GetHashCode() == GetHashCode();
        }
        public override string ToString()
        {
            return Path;
        }
    }
}
