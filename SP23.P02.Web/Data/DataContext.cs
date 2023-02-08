using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.UserRoles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Data;

public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DataContext()
    {
    }
    public DbSet<TrainStation> TrainStation { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    var userRoleBuilder = modelBuilder.Entity<UserRole>();

    //    userRoleBuilder.HasKey(x => new { x.UserId, x.RoleId });

    //    userRoleBuilder.HasOne(x => x.Role)
    //        .WithMany(x => x.Users)
    //        .HasForeignKey(x => x.RoleId);

    //    userRoleBuilder.HasOne(x => x.User)
    //        .WithMany(x => x.Roles)
    //        .HasForeignKey(x => x.UserId);

    //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    //}
}