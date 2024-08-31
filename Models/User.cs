using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class User
    {
        public int id { get; init; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        [JsonIgnore]
        public string password { get; set; } = "";
        public string[] roles { get; set; } = [];
        [JsonIgnore]
        public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        [JsonIgnore]
        public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }
}

