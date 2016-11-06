using System.Threading.Tasks;

namespace Network.Core.Intefaces
{
    public interface IRequestProcessor
    {
        Task<Response> ProcessRequestAsync(Request request);
    }
}
