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
    }
}
