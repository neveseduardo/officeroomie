namespace WebApi.Models;
public class Client {
    public int id { get; init; }
    public string name { get; set; } = "";
    public string email { get; set; } = "";
    public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
}