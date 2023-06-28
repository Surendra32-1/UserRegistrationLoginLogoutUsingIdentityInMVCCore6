using Microsoft.AspNetCore.Identity;

namespace LoginLogoutUsingIdentity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        //public string ? ProfilePicture { get; set; }



    }
}
