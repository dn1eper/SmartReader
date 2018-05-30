using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Library.Book
{
    [Serializable]
    public class BookInfo
    {
        public string Title { get; set; }
        public Int32 Id { get; set; }
        public ulong Offset { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is BookInfo &&
                    obj.GetHashCode() == GetHashCode();
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
