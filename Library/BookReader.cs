using System;
using System.IO;
using System.Text;

namespace Library
{
    class BookReader : IDisposable
    {
        private const int BUFFER_SIZE = 65536;  // 64 Kb

        private FileStream stream;
        private Encoding encoding;

        private byte[] buffer;
        private int bufferOffset;
        /// <summary>
        /// Смещение текущей страницы относительно начала буфера,
        /// реализует автоматическую подгрузку буфера при 
        /// приближении к началу или к концу буфера.
        /// Указывает на начало новой строки.
        /// </summary>
        private int BufferOffset
        {
            get => bufferOffset;
            set
            {
                if (value == 0 && startOfFile)
                {
                    if (encoding == Encoding.Unicode) bufferOffset = 2;
                    else if (encoding == Encoding.UTF8) bufferOffset = 3;
                    else bufferOffset = 0;
                }
                else if (value < BUFFER_SIZE * 0.1 && !startOfFile)
                {
                    // Буферизация следующего участка книги при приближении указателя к концу буфера
                    BaseOffset = value - BUFFER_SIZE / 2;
                    if (BaseOffset < 0)
                    {
                        bufferOffset = value;
                        BaseOffset = 0;
                    }
                    else bufferOffset = BUFFER_SIZE / 2;
                    LoadBuffer();
                }
                else if (value > BUFFER_SIZE * 0.9 && !endOfFile)
                {
                    // Буферизация предыдущего участка книги при приближении указателя к началу буфера
                    BaseOffset = value - BUFFER_SIZE / 3;
                    bufferOffset = BUFFER_SIZE / 3;
                    LoadBuffer();
                }
                else bufferOffset = value;
            }
        }
        /// <summary>
        /// Смещение буфера относительно начала книжки
        /// </summary>
        public int BaseOffset { get; private set; }

        private bool endOfFile => BaseOffset + BUFFER_SIZE >= stream.Length;
        private bool startOfFile => BaseOffset == 0;
        /// <summary>
        /// Является ли последяя строка последней
        /// </summary>
        public bool IsLastLine { get; private set; }

        /// <summary>
        /// Реализует построчное чтение книжки с автоопределением кодировки 
        /// (UTF8, Unicode, Windows-1251)
        /// </summary>
        /// <param name="path">Путь к файлу книжки на диске</param>
        /// <param name="offset">Смещение от начала книги</param>
        public BookReader(FileInfo file, int offset = 0)
        {
            stream = file.OpenRead();
            buffer = new byte[BUFFER_SIZE];
            BaseOffset = offset;
            LoadBuffer();
            encoding = GetEncoding();
            BufferOffset = 0;
            IsLastLine = false;
        }

        /// <summary>
        /// Возвращает очередную строку
        /// </summary>
        /// <param name="offset">Смещение относительно текущей строки (по умолчанию 1 - следующая строка)</param>
        public string ReadLine(int offset = 1)
        {
            int linePosition = GetLine(offset >= 0);
            int lineLength = linePosition - BufferOffset;
            if (linePosition != -1)
            {
                if (offset > 0)
                {
                    if (lineLength == 0)
                    {
                        IsLastLine = true;
                        return "<EOF/>";
                    }
                    // Получаем следующую строку
                    string line = encoding.GetString(buffer, BufferOffset, lineLength);
                    BufferOffset += lineLength;
                    if (offset > 1) return ReadLine(offset - 1);
                    else return line;
                }
                else if (offset < 0)
                {
                    // Получаем предыдущую строку
                    BufferOffset += lineLength;
                    if (linePosition == 0) return "<SOF/>";
                    else if (IsLastLine)
                    {
                        IsLastLine = false;
                        return ReadLine(1);
                    }
                    linePosition = GetLine(false);
                    lineLength = linePosition - BufferOffset;
                    BufferOffset += lineLength;

                    if (offset < -1) return ReadLine(offset + 1);
                    else return ReadLine(1);
                }
                //else return encoding.GetString(buffer, BufferOffset, lineLength);
                else return null;
                
            }
            else return null;
        }

        /// <summary>
        /// Определяет кодировку текста
        /// </summary>
        /// <returns></returns>
        private Encoding GetEncoding()
        {
            if (GetLineUnicode(true, true) != -1)
            {
                //if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                return Encoding.Unicode;
            }
            else if (GetLineUtf(true, true) != -1)
            {
                if (BaseOffset == 0 && buffer[0] == 0xEF 
                    && buffer[1] == 0xBB && buffer[2] == 0xBF)
                    return Encoding.UTF8;
                else return Encoding.Default;
            }
            else return null;
        }

        private void LoadBuffer() => stream.Read(buffer, BaseOffset, BUFFER_SIZE);

        private int GetLineUnicode(bool direction, bool test = false)
        {
            int position = BufferOffset;
            int length = (int)stream.Length;
            if (direction)
            {
                // Поиск конца следующей строки
                do
                {
                    if (buffer[position] == 0xD && buffer[position + 2] == 0xA
                       && buffer[position + 1] == 0 && buffer[position + 3] == 0)
                        return position + 4;
                    if (test && position - BufferOffset > 2048) return -1;
                    position += 2;
                } while (position < length - 3);
            }
            else
            {
                // Поиск конца предыдущей строки
                position -= 3;
                while (position > 3)
                {
                    if (buffer[position] == 0 && buffer[position - 2] == 0
                       && buffer[position - 1] == 0xA && buffer[position - 3] == 0xD)
                        return position + 1;
                    position -= 2;
                }
            }
            // Если дошли до конца или начала файла, то символов конца строки не будет
            if (test) return -1;
            else if (direction && position > length - 3) return length;
            else if (!direction && position < 4) return 0;
            else return -1;
        }
        private int GetLineUtf(bool direction, bool test = false)
        {
            int position = BufferOffset;
            int length = (int)stream.Length;
            if (direction)
            {
                // Поиск конца следующей строки
                do
                {
                    if (buffer[position] == 0xD && buffer[position + 1] == 0xA)
                        return position + 2;
                    if (test && position - BufferOffset > 2048) return -1;
                    position++;
                } while (position < length - 1);
            }
            else
            {
                // Поиск конца предыдущей строки
                position--;
                while (position --> 0)
                {
                    if (buffer[position] == 0xA && buffer[position - 1] == 0xD)
                        return position + 1;
                }
            }
            // Если дошли до конца или начала файла, то символов конца строки не будет
            if (test) return -1;
            else if (direction && position > length - 2) return length;
            else if (!direction && position < 1) return 0;
            else return -1;
        }
        private int GetLineAnsi(bool direction)
        {
            return GetLineUtf(direction);
        }
        private int GetLine(bool direction = true)
        {
            if (encoding == Encoding.Unicode) return GetLineUnicode(direction);
            else if (encoding == Encoding.Default) return GetLineAnsi(direction);
            else if (encoding == Encoding.UTF8) return GetLineUtf(direction);
            else return -1;
        }

        /// <summary>
        /// Освобождает все ресурсы
        /// </summary>
        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
