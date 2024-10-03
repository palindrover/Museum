using Microsoft.AspNetCore.Identity;
using Museum.Contexts;

namespace Museum.Models
{
    public class User : IdentityUser<int>
    {
        private UserContext _context;
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Login { get; set; }
        public string? Pass { get; set; }
        public string? Salt { get; set; }
        public bool Role { get; set; }
    }
}
