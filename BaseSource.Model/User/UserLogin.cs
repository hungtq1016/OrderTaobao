using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public DateTime CreatedAt { get; set; }
    }
}
