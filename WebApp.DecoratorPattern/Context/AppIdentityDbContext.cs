using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.DecoratorPattern.Entities;

namespace WebApp.DecoratorPattern.Context;

public class AppIdentityDbContext : IdentityDbContext<AppUser> {
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) {}

    public DbSet<Product> Products { get; set; }
}
