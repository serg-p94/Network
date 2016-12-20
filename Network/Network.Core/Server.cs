using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using Network.Core.Intefaces;
using System;
using Network.Core.RequestProcessors;
using static Network.Core.RequestProcessors.LambdaRequestProcessor;

namespace Network.Core
{
   public class Server
   {
      private readonly Dictionary<Type, IRequestProcessor> _requestProcessors;
      private readonly TcpListener _tcpListener;

      private List<CommunicationUnit> _clients;

      public bool IsWorking { get; private set; }

      public Server()
      {
         _requestProcessors = new Dictionary<Type, IRequestProcessor>()
         {
            { typeof(Request), new ServerRequestProcessor() }
         };

         _tcpListener = TcpListener.Create(NetworkConfig.Port);
         _clients = new List<CommunicationUnit>();
      }

      public async Task Start()
      {
         IsWorking = true;
         while (IsWorking)
         {
            var tcpClient = await _tcpListener.AcceptTcpClientAsync();
            var unit = new CommunicationUnit(tcpClient, LambdaRequestProcessor.FromLambda((RequestProcessAsync)ProcessRequest));
            _clients.Add(unit);
            unit.Start();
         }
      }

      public void SetRequestProcessor<TRequest>(IRequestProcessor requestProcessor) where TRequest : Request
      {
         _requestProcessors[typeof(TRequest)] = requestProcessor;
      }

      private async Task<Response> ProcessRequest(Request request)
      {
         if (request.Text == "Exit")
         {
            Stop();
            return new Response { Text = "Server Stopped!" };
         }

         var processor = _requestProcessors[typeof(Request)];
         if (processor == null)
         {
            throw new Exception($"Processor for request \'{request.GetType().Name}\' not found.");
         }
         return await processor.ProcessRequestAsync(request);
      }

      public void Stop()
      {
         IsWorking = false;
         _tcpListener.Stop();
      }
   }
}