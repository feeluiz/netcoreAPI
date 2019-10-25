using MongoDB.Driver;
using mongonetAPI.Models;

namespace mongonetAPI.Context
{
    public class Context
    {
        private readonly IMongoDatabase db;

        public Context() 
        {
            db = new MongoClient("mongodb://localhost:27017").GetDatabase("Products");
        }
        public IMongoCollection<Product> Products {
            get => db.GetCollection<Product>("Products");
        }
        
    }
}