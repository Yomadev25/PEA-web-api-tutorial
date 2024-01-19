using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PEA_WebAPI.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
