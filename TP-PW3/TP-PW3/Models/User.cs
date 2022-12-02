using Microsoft.EntityFrameworkCore;

namespace TP_PW3.Models
{
    [Keyless]
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
