using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Context;

public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

    public DbSet<Discount> Discounts { get; init; }
}
