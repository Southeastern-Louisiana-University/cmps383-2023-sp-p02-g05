using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Users
{
    public class User : IdentityUser<int>
    {
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

    }
}
