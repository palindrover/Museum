namespace Museum.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Login { get; set; }
        public string? Pass { get; set; }
        public string? Salt { get; set; }
        public int Role { get; set; }
    }
}
