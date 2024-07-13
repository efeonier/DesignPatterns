using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbApp.ChainResponsibility.Entities;

namespace WebbApp.ChainResponsibility.Context;

public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
}
