using System.Net.Sockets;

namespace ВКанализации
{
    public class User(string name, string? ip = null, Socket? client = null)
    {
        public string Name { get; } = name;
        public string? Ip { get; } = ip;
        public Socket? Client { get; } = client;
    }
}
