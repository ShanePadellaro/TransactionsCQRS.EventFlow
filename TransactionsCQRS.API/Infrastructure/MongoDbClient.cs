using System.Threading.Tasks;
using MongoDB.Driver;

namespace TransactionsCQRS.API.Infrastructure
{
    public class MongoDbClient
    {
        private MongoClient _client;

        public MongoDbClient(string connectionString)
        {
            _client = new MongoClient(connectionString ?? "mongodb://localhost:32768");
        }
        
        public async Task Create(string message)
        {
            var database = _client.GetDatabase("test");
            var collection = database.GetCollection<TestData>("producer");

            await collection.InsertOneAsync(new TestData
            {
                Value = message
            });
        }
    }

    public class TestData
    {
        public string Value { get; set; }
    }
}