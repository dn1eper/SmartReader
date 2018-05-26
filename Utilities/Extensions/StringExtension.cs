using System.IO;

namespace System.Text
{
    public static class StringExtension
    {
        public static Stream ToStream(this string s)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(s));
        }

        public static bool IsEmpty(this string text)
        {
            return text.Trim().Length == 0;
        }
    }
}
