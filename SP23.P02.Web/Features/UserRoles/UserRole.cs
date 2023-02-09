using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Features.UserRoles
{
    public class UserRole : IdentityUserRole<int>
    {
        public Role? Role { get; set; }


        public User? User{ get; set; }
    }
}
