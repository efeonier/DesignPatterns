using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.CompositePattern.Context;
using WebApp.CompositePattern.Entities;

namespace WebApp.CompositePattern;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using var scope = host.Services.CreateScope();

        var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        identityDbContext.Database.Migrate();

        if (!userManager.Users.Any())
        {
            var user1 = new AppUser { UserName = "user1", Email = "user1@outlook.com" };
            userManager.CreateAsync(user1, "Password12*").Wait();
            userManager.CreateAsync(new AppUser { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait();
            userManager.CreateAsync(new AppUser { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait();
            userManager.CreateAsync(new AppUser { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait();
            userManager.CreateAsync(new AppUser { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait();

            var newCategory1 = new Category
            {
                Name = "Romantik",
                RefId = 0,
                UserId = user1.Id
            };
            var newCategory2 = new Category
            {
                Name = "Bilim Kurgu",
                RefId = 0,
                UserId = user1.Id
            };
            var newCategory3 = new Category
            {
                Name = "Polisiye",
                RefId = 0,
                UserId = user1.Id
            };

            identityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);
            identityDbContext.SaveChanges();

            var newSubCategory = new Category
            {
                Name = "Hi-Fi",
                RefId = newCategory2.Id,
                UserId = user1.Id
            };
            var newSubCategory2 = new Category
            {
                Name = "Komedi",
                RefId = newCategory1.Id,
                UserId = user1.Id
            };
            var newSubCategor3 = new Category
            {
                Name = "Suç",
                RefId = newCategory3.Id,
                UserId = user1.Id
            };
            identityDbContext.Categories.AddRange(newSubCategory, newSubCategory2, newSubCategor3);
            identityDbContext.SaveChanges();

            var subCategory = new Category
            {
                Name = "Kara Mizah",
                RefId = newSubCategory2.Id,
                UserId = user1.Id
            };
            identityDbContext.Categories.Add(subCategory);
            identityDbContext.SaveChanges();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
