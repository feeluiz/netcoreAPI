using MongoDB.Bson.Serialization.Attributes;


namespace mongonetAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }  
        public string name { get; set; }  
        public string Description { get; set; }      

    }
    
}