using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Network.Core
{
    class Server
    {
        private readonly TcpListener _tcpListener;
        private readonly BinaryFormatter _binaryFormatter;
        private TcpClient _tcpClient;

        public Server()
        {
            _tcpListener = TcpListener.Create(NetworkConfig.Port);
            _binaryFormatter = new BinaryFormatter();
        }

        public async void Start()
        {
            _tcpClient = await _tcpListener.AcceptTcpClientAsync();
            
        }

        private async Task WorkingProc()
        {
            while (_tcpClient.Connected)
            {
                using (var stream = _tcpClient.GetStream())
                {
                    var request = (Request)_binaryFormatter.Deserialize(stream);
                    // *******
                    // Do smth
                    // *******
                    var response = new Response();
                    _binaryFormatter.Serialize(stream, response);
                }
            }
        }
    }
}
