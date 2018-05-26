using System.Net.Sockets;
using SmartReader.Networking.Implementations;
namespace SmartReader.Networking
{
    public static class NetworkingFactory
    {
        /// <summary>
        /// Создаёт подключение к удалённому серверу.
        /// Должен быть сервер из проекта Server.
        /// </summary>
        /// <param name="host">IP адрес хоста.</param>
        /// <param name="port">Номер порта. Лучше брать больше 1024.</param>
        /// <returns></returns>
        public static IConnection OpenConnection(string host, int port)
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(host, port);
            if (socket.Connected)
            {
                return new SocketConnection(socket);
            }
            throw new SocketException();
        }
        
        /// <summary>
        /// Создаёт слушателя на запускаемой машине по указанному порту.
        /// Порт здесь и порт, передаваемый методу OpenConnection должны совпадать.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IConnectionListener CreateListener(int port)
        {
            return new SocketConnectionListener(port);
        }
    }
}
