namespace WebApi.Models
{
    public class Room
    {
        public int id { get; init; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int capacity { get; set; }
        public int category_id { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";
        public string updated_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";
    }
}
