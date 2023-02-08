using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Features.UserRoles
{
    public class UserRole : IdentityUserRole<int>
    {
        public List<Role>? Role { get; set; }


        public List<User>? User{ get; set; }
    }
}
