using System.Net.Sockets;

namespace Network.Core
{
    public class Client
    {
        private CommunicationUnit _communicationUnit;

        public async void Start()
        {
            var tcpClient = new TcpClient(NetworkConfig.LocalHost, NetworkConfig.Port);
            _communicationUnit = new CommunicationUnit(tcpClient, new ClientRequestProcessor());
            _communicationUnit.Start();
        }
    }
}
