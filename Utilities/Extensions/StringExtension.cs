using System.IO;

namespace System.Text
{
    public static class StringExtension
    {
        public static MemoryStream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
            //return new MemoryStream(Encoding.Default.GetBytes(s));
        }

        public static bool IsEmpty(this string text)
        {
            return text.Trim().Length == 0;
        }
    }
}
