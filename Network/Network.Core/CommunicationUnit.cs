using Network.Core.Intefaces;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Network.Core
{
   public class CommunicationUnit
   {
        private readonly TcpClient _tcpClient;
        private readonly IRequestProcessor _requestProcessor;
        private readonly Serializer _serializer;

        private Task _workingTask;

        public CommunicationUnit(TcpClient tcpClient, IRequestProcessor requestProcessor)
        {
            _tcpClient = tcpClient;
            _requestProcessor = requestProcessor;

            _serializer = new Serializer();
        }

        public void Start()
        {
            _workingTask = Task.Run(async () => await WorkingProc());
        }

        private async Task WorkingProc()
        {
            while (_tcpClient.Connected)
            {
                using (var stream = _tcpClient.GetStream())
                {
                    var request = (Request)await _serializer.DeserializeAsync(stream);
                    var response = await _requestProcessor.ProcessRequestAsync(request);
                    await _serializer.SerializeAsync(stream, response);
                }
            }
        }
   }
}
