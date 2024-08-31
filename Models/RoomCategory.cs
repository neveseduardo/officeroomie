using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class RoomCategory
    {
        public int id { get; init; }
        public string? cescription { get; init; }
        public int user_id { get; set; }
        
        [JsonIgnore]
        public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        
        [JsonIgnore]
        public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";   
    }
}