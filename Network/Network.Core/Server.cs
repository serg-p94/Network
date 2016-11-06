using System.Net.Sockets;
using System.Collections.Generic;

namespace Network.Core
{
    public class Server
    {
        private readonly TcpListener _tcpListener;

        private List<CommunicationUnit> _clients;

        public bool IsWorking { get; private set; }

        public Server()
        {
            _tcpListener = TcpListener.Create(NetworkConfig.Port);
            _clients = new List<CommunicationUnit>();
        }

        public async void Start()
        {
            IsWorking = true;
            while (IsWorking)
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync();
                var unit = new CommunicationUnit(tcpClient, new ServerRequestProcessor());
                _clients.Add(unit);
                unit.Start();
            }
        }
    }
}
