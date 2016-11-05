using Microsoft.VisualStudio.TestTools.UnitTesting;
using Network.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Network.Tests
{
    [TestClass]
    public class SerializerTest
    {
        private Random _random = new Random();

        [TestMethod]
        public async Task TestForAnonimousObject()
        {
            var serializer = new Serializer();
            var obj = new TestData
            {
                Text = "asdasd",
                Number = _random.Next(),
                Date = DateTime.Now
            };

            using (var stream = new MemoryStream())
            {

                await serializer.SerializeAsync(stream, obj);
                stream.Position = 0;
                var resultObj = await serializer.DeserializeAsync<TestData>(stream);

                Assert.AreEqual(obj, resultObj);
            }
        }

        [Serializable]
        private class TestData
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public DateTime Date { get; set; }

            public override bool Equals(object obj)
            {
                var data = obj as TestData;
                return Number == data.Number && Date == data.Date && Text == data.Text;
            }
        }
    }
}