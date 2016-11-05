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
        private readonly Serializer _serializer;
        private TcpClient _tcpClient;

        public Server()
        {
            _tcpListener = TcpListener.Create(NetworkConfig.Port);
            _serializer = new Serializer();
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
                    var request = (Request)await _serializer.DeserializeAsync(stream);
                    // *******
                    // Do smth
                    // *******
                    var response = new Response();
                    await _serializer.SerializeAsync(stream, response);
                }
            }
        }
    }
}
