using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.UserRoles;


namespace SP23.P02.Web.Features.Roles
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole>? User { get; set;}
    }
}
