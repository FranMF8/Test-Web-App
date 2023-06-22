namespace BackendAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
