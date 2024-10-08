namespace WebApi.Models;
public class Reservation
{
    public int id { get; init; }
    public int client_id { get; init; }
    public int room_id { get; set; }
    public string reservation_date { get; set; } = "";
    public string initial_hour { get; set; } = "";
    public string finish_hour { get; set; } = "";
    public string protocol { get; set; } = "";
    public string status { get; set; } = "";
    public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public Client? client { get; set; }
    public Room? room { get; set; }
}