using MongoDB.Bson.Serialization.Attributes;

namespace CollegeERPSystem.Services.Domain.Models
{
    public class Marks
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? SubjectName { get; set; }
        public int? MarksObtained { get; set; }
        public string? Percentage { get; set; }
    }
}
