namespace WebApi.Models;
public class RoomCategory {
    public int id { get; init; }
    public string description { get; init; } = "";
    public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public ICollection<Room> rooms { get; set; } = new List<Room>();
}