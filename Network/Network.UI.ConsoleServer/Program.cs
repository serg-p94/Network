using Network.Core;

namespace Network.UI.ConsoleServer
{
   class Program
   {
      static async void Main(string[] args)
      {
         var server = new Server();
         await server.Start();
      }
   }
}
