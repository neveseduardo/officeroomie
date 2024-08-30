namespace WebApi.Models
{
    public class RoomCategory
    {
        public int id { get; init; }
        public string? cescription { get; init; }
        public int user_id { get; set; }
        public string created_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";
        public string updated_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";   
    }
}