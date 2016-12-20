using Network.Core.Intefaces;
using System;
using System.Threading.Tasks;

namespace Network.Core.RequestProcessors
{
   public class LambdaRequestProcessor : IRequestProcessor
   {
      private RequestProcessAsync _processRequestAsync;
      
      protected LambdaRequestProcessor(RequestProcessFunc processRequestFunc)
      {
         if (processRequestFunc == null)
         {
            throw new NullReferenceException($"{nameof(LambdaRequestProcessor)}: {nameof(processRequestFunc)} can't be null");
         }
         _processRequestAsync = request => Task.Run(() => processRequestFunc(request));
      }

      protected LambdaRequestProcessor(RequestProcessAsync processRequestAsync)
      {
         if (processRequestAsync == null)
         {
            throw new NullReferenceException($"{nameof(LambdaRequestProcessor)}: {nameof(processRequestAsync)} can't be null");
         }
         _processRequestAsync = processRequestAsync;
      }

      public delegate Response RequestProcessFunc(Request request);
      public delegate Task<Response> RequestProcessAsync(Request request);

      public static LambdaRequestProcessor FromLambda(RequestProcessFunc processRequestFunc)
      {
         return new LambdaRequestProcessor(processRequestFunc);
      }

      public static LambdaRequestProcessor FromLambda(RequestProcessAsync processRequestAsync)
      {
         return new LambdaRequestProcessor(processRequestAsync);
      }

      public async Task<Response> ProcessRequestAsync(Request request)
      {
         return await _processRequestAsync(request);
      }
   }
}