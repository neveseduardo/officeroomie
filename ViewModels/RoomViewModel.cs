namespace WebApi.ViewModels;
public class RoomViewModel
{
    public int id { get; init; }
    public string? name { get; set; }
    public string? description { get; set; }
    public int capacity { get; set; }
    public int category_id { get; set; }
}
