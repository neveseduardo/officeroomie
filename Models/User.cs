namespace WebApi.Models
{
    public class User
    {
        public int id { get; init; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string[] roles { get; set; } = [];
        public string created_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";
        public string updated_at { get; set; } = $"{DateTime.Now:HH:mm:ss}";
    }
}

