using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core
{
    public class Serializer
    {
        private readonly BinaryFormatter _binaryFormatter;

        public Serializer()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public async Task<object> DeserializeAsync(Stream stream)
        {
            return await Task.Run(() => _binaryFormatter.Deserialize(stream));
        }
        
        public async Task<TReturn> DeserializeAsync<TReturn>(Stream stream)
        {
            return (TReturn)await DeserializeAsync(stream);
        }

        public async Task SerializeAsync(Stream stream, object obj)
        {
            await Task.Run(() => _binaryFormatter.Serialize(stream, obj));
        }
    }
}
