using Microsoft.AspNetCore.Identity;

namespace ERP.Models
{
    public class User : IdentityUser
    {
        // Temporary raw password input for development
        // TODO: Replace Password with proper built-in PasswordHash
        public string Password { get; set; }
    }
}
