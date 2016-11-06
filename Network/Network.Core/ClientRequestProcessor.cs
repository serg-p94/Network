using Network.Core.Intefaces;
using System;
using System.Threading.Tasks;

namespace Network.Core
{
    public class ClientRequestProcessor : IRequestProcessor
    {
        public async Task<Response> ProcessRequestAsync(Request request)
        {
            return new Response { Date = DateTime.Now, Text = "Message from client" };
        }
    }
}
