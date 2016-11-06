using Network.Core.Intefaces;
using System;
using System.Threading.Tasks;

namespace Network.Core
{
    public class ServerRequestProcessor : IRequestProcessor
    {
        public async Task<Response> ProcessRequestAsync(Request request)
        {
            return new Response
            {
                Date = DateTime.Now,
                Text = $"{DateTime.Now} - Response from Server on request:"
                + Environment.NewLine +
                $"\t{request.Date} - {request.Text}"
            };
        }
    }
}
